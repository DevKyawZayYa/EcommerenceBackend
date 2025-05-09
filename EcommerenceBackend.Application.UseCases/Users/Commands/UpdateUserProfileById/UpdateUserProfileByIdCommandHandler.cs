using AutoMapper;
using EcommerenceBackend.Application.Dto.Users;
using EcommerenceBackend.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Users.Commands.UpdateUserProfileById
{
    // UpdateUserProfileByIdCommandHandler.cs
    public class UpdateUserProfileByIdCommandHandler : IRequestHandler<UpdateUserProfileByIdCommand, UpdateUserProfileByIdResponse>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper; // AutoMapper for mapping DTOs to entities

        public UpdateUserProfileByIdCommandHandler (ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UpdateUserProfileByIdResponse> Handle(UpdateUserProfileByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id == null)
                    throw new ArgumentNullException(nameof(request.Id), "User ID cannot be null.");

                    var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

                    if (user == null)
                    {
                        return new UpdateUserProfileByIdResponse { Success = false, Message = "User not found" };
                    }

                    _mapper.Map(request, user);

                    _dbContext.Users.Update(user);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    return new UpdateUserProfileByIdResponse { Success = true, Message = "User profile updated successfully" };
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error in Update User Profile By Id: {ex.Message}");
                throw;
            }

           
        }
    }

}
