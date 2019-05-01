using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities;
using BackendApartmentReservation.Repositories;

namespace BackendApartmentReservation.Managers
{
    public class GroupManager : IGroupManager
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOfficeRepository _officeRepository;

        public GroupManager(IGroupRepository groupRepository, IEmployeeRepository employeeRepository, IOfficeRepository officeRepository)
        {
            _groupRepository = groupRepository;
            _employeeRepository = employeeRepository;
            _officeRepository = officeRepository;
        }

        public async Task<string> CreateGroup(int officeID, List<DbEmployee> employees)
        {
            var group = new DbGroup();
            var groupId = Guid.NewGuid().ToString();
            group.ExternalGroupId = groupId;

            var office = await _officeRepository.GetOfficeById(officeID);
            group.StartingOffice = office;

            group.Employees = employees.Select(e => new DbEmployeeGroup {DbEmployee = e, DbGroup = group }).ToList();

            await _groupRepository.CreateGroup(group);
            return groupId;
        }

        public async Task AddEmployeeToGroup(string groupID, string emplID)
        { 
            await _groupRepository.AddEmployeeToGroup(groupID, emplID);
        }
    }
}
