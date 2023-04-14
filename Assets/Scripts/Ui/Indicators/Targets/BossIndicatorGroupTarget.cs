using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Ui.Indicators.Targets
{
    public class BossIndicatorGroupTarget : IndicatorGroupTargetBase
    {
        protected override void Awake()
        {
            _indicatorGroupObjectName = "BossIndicators";

            base.Awake();
        }
    }
}
