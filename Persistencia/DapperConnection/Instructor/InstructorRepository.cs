using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Persistencia.Interfaces;

namespace Persistencia.DapperConnection.Instructor
{
    public class InstructorRepository : IInstructor
    {
        private readonly IFactoryConnection _factoryConnection;
        public InstructorRepository(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }

        public async Task<IEnumerable<InstructorModel>> ObtainList()
        {
            IEnumerable<InstructorModel> instructorList = null;
            var storeProcedure = "usp_Obtain_Instructors";
            try
            {   
                var connection = _factoryConnection.GetConnection();
                instructorList = await connection.QueryAsync<InstructorModel>(storeProcedure, null, commandType: CommandType.StoredProcedure);
            }
            catch(Exception ex)
            {
                throw new Exception("Someting wrong, an error ocurred in the data", ex);
            }
             finally
            {
                _factoryConnection.CloseConexion();
            }

            return instructorList;
        }

        public async Task<InstructorModel> ObtainById(Guid id)
        {
             var storeProcedure = "usp_obtain_instructor_by_id";
             InstructorModel instructor = null;
             try{
                 var connection = _factoryConnection.GetConnection();
                 instructor = await connection.QueryFirstAsync<InstructorModel>(
                     storeProcedure,
                     new{
                         Id = id
                     },
                     commandType: CommandType.StoredProcedure
                 );
                    _factoryConnection.CloseConexion();

                return instructor;

             }catch(Exception ex){
                 throw new Exception("Something wrong, dont found instructor",ex);
             }
        }



        public async Task<int> New(string firstName, string lastName, string grade)
        {
            var storeProcedure = "usp_instructor_new";
            try
            {
                var connection = _factoryConnection.GetConnection();
                var result = await connection.ExecuteAsync(
                    storeProcedure,
                    new
                    {
                        InstructorId = Guid.NewGuid(),
                        Name = firstName,
                        LastName = lastName,
                        Grade = grade
                    },
                commandType: CommandType.StoredProcedure
                );
                _factoryConnection.CloseConexion();

                return result;
            }
            catch (Exception ex)
            {
                
                 throw new Exception("Someting wrong, dont save new instructor", ex);
            }
        }





        public async Task<int> Update(Guid instructorId, string firstName, string lastName, string grade)
        {
            var storeProcedure = "usp_instructor_editor";
            try{
                var connection = _factoryConnection.GetConnection();
                var result = await connection.ExecuteAsync(
                    storeProcedure,
                    new{
                        InstructorId = instructorId,
                        Name = firstName,
                        LastName = lastName,
                        Grade = grade
                    }, commandType: CommandType.StoredProcedure
                );
                _factoryConnection.CloseConexion();
                return result;
            }catch(Exception ex){
                throw new Exception("Someting wrong, dont update  instructor", ex);
            }
        }

        public async Task<int> Delete(Guid id)
        {
            var storeProcedure = "usp_instructor_delete";
            try
            {
                var connection = _factoryConnection.GetConnection();
                var result = await connection.ExecuteAsync(
                    storeProcedure,
                    new
                    {
                        InstructorId = id,
                    },
                commandType: CommandType.StoredProcedure
                );
                _factoryConnection.CloseConexion();

                return result;

            }
            catch (Exception ex)
            {
                
                throw new Exception("Someting wrong, dont delete instructor", ex);
            }
        }
    }
}