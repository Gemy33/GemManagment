using GemManagment.BLL.ViewModels.Analytic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.Services.Interfaces
{
    public interface IAnlyticService
    {
        AnalyticsViewModel GetAnalyticsData();
    }
}
