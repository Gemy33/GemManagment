using GemManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.DAL.Repositorys.Interfaces
{
    public interface IMemberShipRepo : IGenericRepo<MemberPlan>
    {
        List<MemberPlan> GetMembers();
        MemberPlan? GetMember(int memberid , int planid);
    }
}
