using DigitalMicrowave.Domain.Entities;
using DigitalMicrowave.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Configuration;
using Dapper;

namespace DigitalMicrowave.Infrastructure.Repositories
{
    public class HeatingProgramRepository : IHeatingProgramRepository
    {
        private IDbConnection Connection =>
            new SqlConnection(ConfigurationManager.ConnectionStrings["DigitalMicrowave"].ConnectionString);
       
        public bool CharacterExists(string character, int? ignoreId = null)
        {
            using (var db = Connection)
            {
                string sql = "SELECT COUNT(1) FROM HeatingPrograms WHERE HeatingCharacteristic = @character";
                if (ignoreId != null)
                    sql += " AND Id <> @ignoreId";

                return db.ExecuteScalar<int>(sql, new { character, ignoreId }) > 0;
            }
        }

        public void Create(HeatingProgram program)
        {
            program.ProgramDefault = false;

            using (var db = Connection)
            {
                string sql = @"
            INSERT INTO HeatingPrograms 
            (ProgramName, Food, Time, Power, HeatingCharacteristic, Instructions, ProgramDefault)
            VALUES (@ProgramName, @Food, @Time, @Power, @HeatingCharacteristic, @Instructions, 0)";
                db.Execute(sql, program);
            }
        }

        public void Delete(int id)
        {
            var existing = GetById(id);
            if (existing.ProgramDefault)
                throw new Exception("Programas pré-definidos não podem ser excluídos!");

            using (var db = Connection)
            {
                db.Execute("DELETE FROM HeatingPrograms WHERE Id=@Id AND ProgramDefault = 0", new { Id = id });
            }
        }

        public IEnumerable<HeatingProgram> GetAll()
        {
            using (var db = Connection)
            {
                return db.Query<HeatingProgram>("SELECT * FROM HeatingPrograms").ToList();
            }
        }

        public HeatingProgram GetById(int id)
        {
            using (var db = Connection)
            {
                return db.QueryFirstOrDefault<HeatingProgram>(
                "SELECT * FROM HeatingPrograms WHERE Id = @id", new { id });
            }                
        }

        public void Update(HeatingProgram program)
        {
            var existing = GetById(program.Id);
            if (existing.ProgramDefault)
                throw new Exception("Programas pré-definidos não podem ser alterados!");

            using (var db = Connection)
            {
                string sql = @"
                UPDATE HeatingPrograms SET
                    ProgramName=@ProgramName,
                    Food=@Food,
                    Time=@Time,
                    Power=@Power,
                    HeatingCharacteristic=@HeatingCharacteristic,
                    Instructions=@Instructions
                WHERE Id=@Id AND ProgramDefault = 0";
                    db.Execute(sql, program);
            }
        }
    }
}
