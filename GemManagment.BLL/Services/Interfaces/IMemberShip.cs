using GemManagment.BLL.ViewModels.MemberShip;
using GemManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.Services.Interfaces
{
    public interface IMemberShip
    {
       IEnumerable<MemberPlan> GetAllMembers();
       bool Create(CreateMemberShipViewModel createmembership);
        bool Cancel(int memberid , int planid);
    }
}
