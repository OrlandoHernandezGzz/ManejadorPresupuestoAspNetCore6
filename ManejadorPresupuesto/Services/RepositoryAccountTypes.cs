﻿using Dapper;
using ManejadorPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejadorPresupuesto.Services
{
    public class RepositoryAccountTypes : IRepositoryAccountTypes
    {
        private readonly string _connectionString;

        public RepositoryAccountTypes(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Create(AccountType accountType)
        {
            using var connection = new SqlConnection(_connectionString);

            // QuerySingle: Permite hacer un query que estoy seguro que me va a traer un simple resultado.
            // Esto porque yo quiero extraer despues de insertar el tipo cuenta traerme el id de ese tipo de cuenta.
            var id = await connection.QuerySingleAsync<int>($@"INSERT INTO TiposCuentas (Nombre, UsuarioId, Orden)
                                                VALUES (@Nombre, @UsuarioId, 0);
                                                
                                                SELECT SCOPE_IDENTITY();
                                                ", accountType);

            accountType.Id = id;
        }

        public async Task<bool> Exist(string name, int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var exist = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM TiposCuentas 
                                                WHERE Nombre = @name AND UsuarioId = @userId;", new { name, userId });

                return exist == 1;
            }
        }

    }
}
