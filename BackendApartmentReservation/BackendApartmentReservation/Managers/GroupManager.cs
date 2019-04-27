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
            var office = await _officeRepository.GetOfficeById(officeID);
            
            group.StartingOffice = office;
            group.Employees = new List<DbEmployeeGroup>();

            foreach (DbEmployee e in employees)
            {
                group.Employees.Add(
                new DbEmployeeGroup
                {
                    DbEmployee = e,
                    DbGroup = group
                });
            }

            await _groupRepository.CreateGroup(group);
            return group.Id.ToString();
        }

        public async Task AddEmployeeToGroup(int groupID, int emplID)
        { 
            await _groupRepository.AddEmployeeToGroup(groupID, emplID);
        }
    }
}
