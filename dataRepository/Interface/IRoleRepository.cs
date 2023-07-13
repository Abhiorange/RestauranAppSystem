using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Models;

namespace dataRepository.Interface
{
    public interface IRoleRepository
    {
        public int addrole(RoleRegisterVm model);
        public List<RoleInfoVm> GetRoleList();

        public RoleById RoleById(int id);

        public int EditRole(RoleById model);
        public int deleteRoleById(int id);


    }
}