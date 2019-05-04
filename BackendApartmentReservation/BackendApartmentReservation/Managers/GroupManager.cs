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

            foreach (DbEmployee employee in employees)
            {
                var employeeGroup = new DbEmployeeGroup{ DbEmployee = employee, DbGroup = group };
                employeeGroup.DbEmployee = employee;
                employeeGroup.DbGroup = group;
                await _groupRepository.CreateEmployeeGroup(employeeGroup);
            }
            
            return groupId;
        }

        public async Task AddEmployeeToGroup(string groupID, string employeeID)
        {
            var group = await _groupRepository.GetGroupById(groupID);
            var employee = await _employeeRepository.GetEmployeeByEmployeeId(employeeID);
            var employeeGroup = new DbEmployeeGroup();
            employeeGroup.DbGroup = group;
            employeeGroup.DbEmployee = employee;

            await _groupRepository.CreateEmployeeGroup(employeeGroup);
        }
    }
}
