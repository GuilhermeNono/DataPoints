﻿using DataPoints.Domain.Entities.Audit;
using DataPoints.Domain.Repositories.Audit;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;

namespace DataPoints.Infrastructure.Persistence.Audit;

public class UserLogRepository(AuditContext context) : AuditRepository<UserLogEntity, long>(context), IUserLogRepository
{
    
}
