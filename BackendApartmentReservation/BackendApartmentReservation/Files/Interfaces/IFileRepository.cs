﻿namespace BackendApartmentReservation.Employees.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database.Entities;

    public interface IFileRepository
    {
        Task<DbFile> GetFileById(int fileID);

        Task CreateFile(DbFile dbFile);

        Task UpdateFile(DbFile dbFile);

        Task DeleteFile(DbFile dbFile);
    }
}