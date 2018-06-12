using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ETModel;
using MongoDB.Bson;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using UnityEditor;
using UnityEngine;

public struct CellInfo
{
	public string Type;
	public string Name;
	public string Desc;
}

public enum ExcelType
{
    Client,
    Public,
    Server,
    Default,
}

public class ExcelMD5Info
{
	public Dictionary<string, string> fileMD5 = new Dictionary<string, string>();

	public string Get(string fileName)
	{
		string md5 = "";
		this.fileMD5.TryGetValue(fileName, out md5);
		return md5;
	}

	public void Add(string fileName, string md5)
	{
		this.fileMD5[fileName] = md5;
	}
}

public class ExcelExporterEditor : EditorWindow
{
	[MenuItem("Tools/导出配置")]
	private static void ShowWindow()
	{
		GetWindow(typeof(ExcelExporterEditor));
	}

	private const string ExcelPath = @"..\Excel";
    private const string _clientExcelPartPath = @"\Excel\Client";
    private const string _publicExcelPartPath = @"\Excel\Public";
    private const string _serverExcelPartPath = @"\Excel\Server";

    private const string _clientExcelPath = @"..\Excel\Client";
    private const string _publicExcelPath = @"..\Excel\Public";
    private const string _serverExcelPath = @"..\Excel\Server";

    private const string _clientExportPath = @"..\Config\Client";
    private const string _publicExportPath = @"..\Config\Public";
    private const string _serverExportPath = @"..\Config\Server";

    private const string _clientCopyPath = @".\Assets\Res\Config\";
    private const string _serverCopyPath = @"..\Server\Config\";


    private const string _clientClassPath = @".\Assets\Scripts\Entity\Config";
    private const string _serverClassPath = @"..\Server\Model\\Entity\Config";

    private ExcelMD5Info md5Info;

    private ExcelType _excelType = ExcelType.Default;

    // Update is called once per frame
    private void OnGUI()
	{
		try
		{

            if (GUILayout.Button("导出客户端配置"))
			{
                _excelType = ExcelType.Client;
				ExportAll();
			}

			if (GUILayout.Button("导出服务端配置"))
			{
                _excelType = ExcelType.Server;
                ExportAll();
			}

			if (GUILayout.Button("生成配置类"))
			{
                //ExportAllClass(@".\Assets\Scripts\Entity\Config");
                ExportAllClass();
            }
		}
		catch (Exception e)
		{
			Log.Error(e);
		}
	}

	private void ExportAllClass()
	{
        string[] tempArr = Directory.GetFiles(ExcelPath, "*.xlsx", SearchOption.AllDirectories);

        foreach (string filePath in tempArr)
		{
			if (Path.GetExtension(filePath) != ".xlsx")
			{
				continue;
			}
			if (Path.GetFileName(filePath).StartsWith("~"))
			{
				continue;
			}


            if (filePath.Contains(_clientExcelPartPath))
            {
                ExportClass(filePath, _clientClassPath);

            }
            else if (filePath.Contains(_serverExcelPartPath))
            {
                ExportClass(filePath, _serverClassPath);

            }
            else if(filePath.Contains(_publicExcelPartPath))
            {
                _excelType = ExcelType.Client;
                ExportClass(filePath, _clientClassPath);
                _excelType = ExcelType.Server;
                ExportClass(filePath, _serverClassPath);

            }

        }
		Log.Debug("生成类完成!");
		AssetDatabase.Refresh();
	}

	private void ExportClass(string fileName, string exportDir)
	{
		XSSFWorkbook xssfWorkbook;
		using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
		{
			xssfWorkbook = new XSSFWorkbook(file);
		}

		string protoName = Path.GetFileNameWithoutExtension(fileName);
		Log.Info($"{protoName}生成class开始");
		string exportPath = Path.Combine(exportDir, $"{protoName}.cs");
		using (FileStream txt = new FileStream(exportPath, FileMode.Create))
		using (StreamWriter sw = new StreamWriter(txt))
		{
			StringBuilder sb = new StringBuilder();
			ISheet sheet = xssfWorkbook.GetSheetAt(0);
			sb.Append("namespace ETModel\n{\n");

            sb.Append("\t[Config(AppType.Client)]\n");

            sb.Append($"\tpublic partial class {protoName}Category : ACategory<{protoName}>\n");
			sb.Append("\t{\n");
			sb.Append("\t}\n\n");

			sb.Append($"\tpublic class {protoName}: IConfig\n");
			sb.Append("\t{\n");
			sb.Append("\t\tpublic long Id { get; set; }\n");

			int cellCount = sheet.GetRow(3).LastCellNum;
			
			for (int i = 2; i < cellCount; i++)
			{
				string fieldDesc = GetCellString(sheet, 2, i);

				if (fieldDesc.StartsWith("#"))
				{
					continue;
				}

				// s开头表示这个字段是服务端专用
				if (fieldDesc.StartsWith("s") && _excelType == ExcelType.Client)
				{
					continue;
				}
				
				string fieldName = GetCellString(sheet, 3, i);

				if (fieldName == "Id" || fieldName == "_id")
				{
					continue;
				}

				string fieldType = GetCellString(sheet, 4, i);
				if (fieldType == "" || fieldName == "")
				{
					continue;
				}

				sb.Append($"\t\tpublic {fieldType} {fieldName};\n");
			}

			sb.Append("\t}\n");
			sb.Append("}\n");

			sw.Write(sb.ToString());
		}
	}


	private void ExportAll()
	{
		string md5File = Path.Combine(ExcelPath, "md5.txt");
		if (!File.Exists(md5File))
		{
			this.md5Info = new ExcelMD5Info();
		}
		else
		{
			this.md5Info = MongoHelper.FromJson<ExcelMD5Info>(File.ReadAllText(md5File));
		}

        string[] tempArr = Directory.GetFiles(ExcelPath, "*.xlsx", SearchOption.AllDirectories);
        

        foreach (string filePath in tempArr)
		{
			if (Path.GetExtension(filePath) != ".xlsx")
			{
				continue;
			}
			if (Path.GetFileName(filePath).StartsWith("~"))
			{
				continue;
			}

            string eliminatePath = "";
            string exportDir = "";
            switch (_excelType)
            {
                case ExcelType.Client:
                    if (filePath.Contains(_publicExcelPartPath))
                    {
                        exportDir = _publicExportPath;
                    }
                    else
                    {
                        exportDir = _clientExportPath;
                    }
                    eliminatePath = _serverExcelPartPath;
                    break;
                case ExcelType.Public:
                    break;
                case ExcelType.Server:
                    if (filePath.Contains(_publicExcelPartPath))
                    {
                        exportDir = _publicExportPath;
                    }
                    else
                    {
                        exportDir = _serverExportPath;
                    }
                    eliminatePath = _clientExcelPartPath;
                    break;
            }
            if (filePath.Contains(eliminatePath)) continue;

            string fileName = Path.GetFileName(filePath);
            string oldMD5 = this.md5Info.Get(fileName);
            string md5Key = _excelType + "-" + fileName;
            string md5 = MD5Helper.FileMD5(filePath);
            this.md5Info.Add(md5Key, md5);
            if (md5 == oldMD5)
            {
                continue;
            }

            Export(filePath, exportDir);

            if (_excelType == ExcelType.Client)
            {
                string protoName = Path.GetFileNameWithoutExtension(fileName);
                string sourcePath = Path.Combine(exportDir, $"{protoName}.txt");
                string destPath = Path.Combine(_clientCopyPath, $"{protoName}.txt");
                if (File.Exists(destPath))
                {
                    File.Delete(destPath);
                }
                File.Copy(sourcePath, destPath);
            }

            else if (_excelType == ExcelType.Server)
            {
                string protoName = Path.GetFileNameWithoutExtension(fileName);
                string sourcePath = Path.Combine(exportDir, $"{protoName}.txt");
                string destPath = Path.Combine(_serverCopyPath, $"{protoName}.txt");
                if (File.Exists(destPath))
                {
                    File.Delete(destPath);
                }
                File.Copy(sourcePath, destPath);
            }
        }

		File.WriteAllText(md5File, this.md5Info.ToJson());

		Log.Info("所有表导表完成");
		AssetDatabase.Refresh();
	}

	private void Export(string fileName, string exportDir)
	{
		XSSFWorkbook xssfWorkbook;
		using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
		{
			xssfWorkbook = new XSSFWorkbook(file);
		}
		string protoName = Path.GetFileNameWithoutExtension(fileName);
		Log.Info($"{protoName}导表开始");
		string exportPath = Path.Combine(exportDir, $"{protoName}.txt");
		using (FileStream txt = new FileStream(exportPath, FileMode.Create))
		using (StreamWriter sw = new StreamWriter(txt))
		{
			for (int i = 0; i < xssfWorkbook.NumberOfSheets; ++i)
			{
				ISheet sheet = xssfWorkbook.GetSheetAt(i);
				ExportSheet(sheet, sw);
			}
		}
		Log.Info($"{protoName}导表完成");
	}

	private void ExportSheet(ISheet sheet, StreamWriter sw)
	{
		int cellCount = sheet.GetRow(3).LastCellNum;

		CellInfo[] cellInfos = new CellInfo[cellCount];

		for (int i = 2; i < cellCount; i++)
		{
			string fieldDesc = GetCellString(sheet, 2, i);
			string fieldName = GetCellString(sheet, 3, i);
			string fieldType = GetCellString(sheet, 4, i);
			cellInfos[i] = new CellInfo() { Name = fieldName, Type = fieldType, Desc = fieldDesc };
		}
		
		for (int i = 5; i <= sheet.LastRowNum; ++i)
		{
			if (GetCellString(sheet, i, 2) == "")
			{
				continue;
			}
			StringBuilder sb = new StringBuilder();
			sb.Append("{");
			IRow row = sheet.GetRow(i);
			for (int j = 2; j < cellCount; ++j)
			{
				string desc = cellInfos[j].Desc.ToLower();
				if (desc.StartsWith("#"))
				{
					continue;
				}

				// s开头表示这个字段是服务端专用
				if (desc.StartsWith("s") && _excelType == ExcelType.Client)
				{
					continue;
				}

				// c开头表示这个字段是客户端专用
				if (desc.StartsWith("c") && _excelType == ExcelType.Server)
				{
					continue;
				}

				string fieldValue = GetCellString(row, j);
				if (fieldValue == "")
				{
					throw new Exception($"sheet: {sheet.SheetName} 中有空白字段 {i},{j}");
				}

				if (j > 2)
				{
					sb.Append(",");
				}

				string fieldName = cellInfos[j].Name;

				if (fieldName == "Id" || fieldName == "_id")
				{
					if (_excelType == ExcelType.Client)
					{
						fieldName = "Id";
					}
					else
					{
						fieldName = "_id";
					}
				}

				string fieldType = cellInfos[j].Type;
				sb.Append($"\"{fieldName}\":{Convert(fieldType, fieldValue)}");
			}
			sb.Append("}");
			sw.WriteLine(sb.ToString());
		}
	}

	private static string Convert(string type, string value)
	{
		switch (type)
		{
			case "int[]":
			case "int32[]":
			case "long[]":
				return $"[{value}]";
			case "string[]":
				return $"[{value}]";
			case "int":
			case "int32":
			case "int64":
			case "long":
			case "float":
			case "double":
				return value;
			case "string":
				return $"\"{value}\"";
			default:
				throw new Exception($"不支持此类型: {type}");
		}
	}

	private static string GetCellString(ISheet sheet, int i, int j)
	{
		return sheet.GetRow(i)?.GetCell(j)?.ToString() ?? "";
	}

	private static string GetCellString(IRow row, int i)
	{
		return row?.GetCell(i)?.ToString() ?? "";
	}

	private static string GetCellString(ICell cell)
	{
		return cell?.ToString() ?? "";
	}
}
