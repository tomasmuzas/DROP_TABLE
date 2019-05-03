using System;
using System.Collections.Generic;
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

            await _groupRepository.CreateGroup(group);

            foreach (DbEmployee e in employees)
            {
                var employeeGroup = new DbEmployeeGroup{ DbEmployee = e, DbGroup = group };
                employeeGroup.DbEmployee = e;
                employeeGroup.DbGroup = group;
                await _groupRepository.CreateEmployeeGroup(employeeGroup);
            }
            
            return groupId;
        }

        public async Task AddEmployeeToGroup(string groupID, string emplID)
        {
            var group = await _groupRepository.GetGroupById(groupID);
            var empl = await _employeeRepository.GetEmployeeByEmployeeId(emplID);
            var employeeGroup = new DbEmployeeGroup();
            employeeGroup.DbGroup = group;
            employeeGroup.DbEmployee = empl;

            await _groupRepository.CreateEmployeeGroup(employeeGroup);
        }
    }
}
