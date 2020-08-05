using System;
using System.Threading;
using System.Threading.Tasks;
using Dominio.Entities;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Comments
{
    public class New
    {
        public class Execute : IRequest
        {
            public string Alumn { get; set; }
            public int Score { get; set; }
            public string Comment { get; set; }

            public Guid CourseId { get; set; }

        }

        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.Alumn).NotEmpty();
                RuleFor(x => x.Score).NotEmpty();
                RuleFor(x => x.Comment).NotEmpty();
                RuleFor(x => x.CourseId).NotEmpty();
            }
        }

        private class Handler : IRequestHandler<Execute>
        {
            private readonly CoursesOnlineContext _context;
            public Handler(CoursesOnlineContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var comment = new Comment {
                    CommentId = Guid.NewGuid(),
                    Alumn = request.Alumn,
                    Score = request.Score,
                    TextComment = request.Comment,
                    CourseId = request.CourseId,
                    CreationDate = DateTime.UtcNow
                };

                _context.Comment.Add(comment);

                var results = await _context.SaveChangesAsync();
                if(results>0){
                    return Unit.Value;
                }

                throw new Exception("Dont insert comment");
            }
        }
    }
}