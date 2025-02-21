using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Application.Members.Commands.Profile.Role;
using DataPoints.Application.Members.Commands.Wallets.Insert;
using DataPoints.Contract.Controller.Authentication.SignUp.Responses;
using DataPoints.Crosscutting.Exceptions.Http.BadRequest;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Users;
using DataPoints.Domain.Entities.Audit;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Enums;
using DataPoints.Domain.Helpers;
using DataPoints.Domain.Repositories.Audit;
using DataPoints.Domain.Repositories.Main;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Premios.Domain.Helpers;

namespace DataPoints.Application.Members.Commands.Authentication.SignUp;

public class SignUpCommandHandler : ICommandHandler<SignUpCommand, SignUpResponse>
{
    private readonly UserManager<UserEntity> _userManagerRepository;

    private readonly IUserRepository _userRepository;
    private readonly IUserLogRepository _userLogRepository;

    private readonly IPersonRepository _personRepository;
    private readonly IPersonLogRepository _personLogRepository;
    
    private readonly ISender _sender;

    public SignUpCommandHandler(IPersonRepository personRepository,
        IUserLogRepository userLogRepository, IPersonLogRepository personLogRepository, IUserRepository userRepository, UserManager<UserEntity> userManagerRepository, ISender sender)
    {
        _personRepository = personRepository;
        _userLogRepository = userLogRepository;
        _personLogRepository = personLogRepository;
        _userRepository = userRepository;
        _userManagerRepository = userManagerRepository;
        _sender = sender;
    }

    public async Task<SignUpResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var document = ValidHelper.DocumentValidate(request.DocumentNumber);

        if (!document.IsFormatedDocumentNumber)
            throw new DocumentNumberHasToBeFormatedException(request.DocumentNumber);

        DocumentValidations(document);

        var user = new UserEntity
        {
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            NormalizedEmail = request.Email.ToUpper(),
        };

        var userByEmail = await _userRepository.FindByNormalizedEmail(request.Email.ToUpper());

        var userByDocument = await _personRepository.FindByDocument(document.Normalized);

        if (userByEmail is { IsActive: true })
            throw new EmailIsAlreadyInUseException(request.Email);

        if (userByDocument is { IsActive: true })
            throw new DocumentIsAlreadyInUseException(request.DocumentNumber);

        await _userManagerRepository.CreateAsync(user);
        await _userLogRepository.Add(new UserLogEntity(user), request.LoggedPerson.Name, cancellationToken);
        
        var personEntity = new PersonEntity()
        {
            Id = user.Id,
            DocumentNumber = request.DocumentNumber,
            NormalizedDocumentNumber = document.Normalized,
            DocumentType = document.PersonDocument,
            BirthDate = request.BirthDate,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PersonType = document.PersonType
        };
        
        await _personRepository.Add(personEntity, request.LoggedPerson.Name, cancellationToken);
        await _personLogRepository.Add(new PersonLogEntity(personEntity), request.LoggedPerson.Name, cancellationToken);

        await _sender.Send(new ProfileInsertRoleCommand(user.Id, RoleHelper.User, request.LoggedPerson), cancellationToken);

        await _sender.Send(new WalletInsertCommand(user.Id, request.LoggedPerson), cancellationToken);

        return new SignUpResponse(user.Id);
    }

    private static void DocumentValidations(DocumentHelper document)
    {
        switch (document.PersonDocument)
        {
            case DocumentType.Cpf when !document.IsCpfValid():
            {
                throw new DocumentCpfInvalidException();
            }
            case DocumentType.Cnpj when !document.IsCnpjValid():
            {
                throw new DocumentCnpjInvalidException();
            }
        }
    }
}
