﻿using Domain.Entities;
namespace Application.Common.Interfaces.Persistence;

public interface IFileRepository
{
    List<FileApp>? GetAllFiles();

}