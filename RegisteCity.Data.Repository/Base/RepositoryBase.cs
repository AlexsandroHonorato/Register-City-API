using RegisteCity.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegisteCity.Data.Repository.Base {
    public class RepositoryBase : IRepositoryBase{
        private string _connectionString;
        public string ConnectionString {
            get { return _connectionString; }
        }

        public RepositoryBase(string connectionString) {
            _connectionString = connectionString;
        }
    }
}
