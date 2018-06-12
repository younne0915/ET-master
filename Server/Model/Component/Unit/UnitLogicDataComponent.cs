using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class UnitLogicDataComponentAwakeSystem : AwakeSystem<UnitLogicDataComponent>
    {
        public override void Awake(UnitLogicDataComponent self)
        {
            self.Awake();
        }
    }

    public class UnitLogicDataComponent : Component
    {
        private Dictionary<int, Hero> _heroDic = new Dictionary<int, Hero>();

        public void Awake()
        {
            LoadData();
        }

        private void LoadData()
        {

        }
    }
}
