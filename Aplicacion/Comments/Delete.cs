using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Exceptions;
using MediatR;
using Persistencia;

namespace Aplicacion.Comments
{
    public class Delete
    {
        public class Execute : IRequest {
            public Guid Id {get;set;}
        }

        public class Handler : IRequestHandler<Execute>
        {
            private readonly CoursesOnlineContext _context;
            public Handler(CoursesOnlineContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                   var comment = await _context.Comment.FindAsync(request.Id);
                if(comment==null){
                    throw new HandlerException(HttpStatusCode.NotFound, new {message="Dont found comment"});
                }

                _context.Remove(comment);
                var result = await _context.SaveChangesAsync();
                if(result>0){
                    return Unit.Value;
                }

                throw new Exception("Something wrong, dont delete comment");
            }
        }
    }
}