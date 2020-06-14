//using myFacility.Services.Contract;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace myFacility.Services.Handler
//{
//    public class PasswordManangementServices : IPasswordManagementService
//    {
//        private IRepository<TCoreUsersPassword> _userdefaultpasswordRepository;
//        private IRepository<TCoreUsersPasswordHist> _useroldpasswordRepository;
//        private IRepository<PasswordOptions> _passwordOptionRepository;
//        private OSMLiteDBContext _context;
//        private readonly IConfiguration _configura;


//        public PasswordManangementServices(IRepository<TCoreUsersPassword> userdefaultpasswordRepository, IRepository<TCoreUsersPasswordHist> useroldpasswordRepository,
//            IRepository<PasswordOptions> passwordOptionRepository, OSMLiteDBContext context, IConfiguration configura)
//        {
//            _userdefaultpasswordRepository = userdefaultpasswordRepository;
//            _useroldpasswordRepository = useroldpasswordRepository;
//            _passwordOptionRepository = passwordOptionRepository;
//            _context = context;
//            _configura = configura;
//        }

//        #region User Default Passwords
//        public void CreateUserDefaultPassword(TCoreUsersPasswordObject obj)
//        {
//            try
//            {
//                if (obj.PwdEncrypt != string.Empty)
//                {
//                    //var entity = Mapper.Map<TUserDefaultPasswordObject, TUserDefaultPassword>(obj);
//                    TCoreUsersPassword entity = new TCoreUsersPassword
//                    {
//                        UserId = obj.UserId,
//                        PwdEncrypt = obj.PwdEncrypt,
//                        PwdExpiryDate = obj.PwdExpiryDate,
//                        CumulativeLogins = obj.CumulativeLogins,
//                        InvalidLogins = obj.InvalidLogins,
//                        ForcePwdChange = obj.ForcePwdChange,
//                        LockedOut = obj.LockedOut,
//                        LastLogin = null,
//                        PwdChangedDate = null,
//                        LastModified = null,
//                        CreatedBy = obj.CreatedBy,
//                        CreatedDate = obj.CreatedDate,
//                        LockoutDate = null,
//                        ModifiedBy = null,
//                        SuccessfulLogins = obj.SuccessfulLogins
//                    };
//                    _userdefaultpasswordRepository.Insert(entity);
//                    _userdefaultpasswordRepository.Save();

//                }
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }

//        public void CreateUserOldPassword(TCoreUsersPasswordHistObject obj)
//        {
//            try
//            {
//                if (obj.PwdEncrypt != string.Empty)
//                {
//                    obj.LastModified = DateTime.Now;
//                    var entity = Mapper.Map<TCoreUsersPasswordHistObject, TCoreUsersPasswordHist>(obj);

//                    _useroldpasswordRepository.Insert(entity);
//                    _useroldpasswordRepository.Save();

//                }
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }
//        #endregion

//        #region Delete Password
//        public bool DeleteUserDefaultPassword(TCoreUsersPasswordObject obj)
//        {
//            var entity = _context.TCoreUsersPassword.Find(obj.UserId);
//            try
//            {
//                if (entity != null)
//                {
//                    ///entity.IsDeleted = true;
//                    entity.LastModified = DateTime.Now;
//                    _userdefaultpasswordRepository.Update(entity);
//                    _userdefaultpasswordRepository.Save();
//                    return true;
//                }
//                else
//                {
//                    throw new InvalidOperationException();
//                }

//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }

//        public bool DeleteUserOldPassword(TCoreUsersPasswordHistObject obj)
//        {
//            throw new NotImplementedException();
//        }
//        #endregion

//        #region  Get Password
//        public TCoreUsersPasswordObject GetUserDefaultPassword(int? id)
//        {
//            try
//            {
//                if (id.HasValue)
//                {
//                    var entity = _context.TCoreUsersPassword.Find(id.Value);

//                    TCoreUsersPasswordObject obj = new TCoreUsersPasswordObject
//                    {
//                        UserId = entity.UserId,
//                        PwdEncrypt = entity.PwdEncrypt,
//                        PwdExpiryDate = entity.PwdExpiryDate,
//                        CumulativeLogins = entity.CumulativeLogins,
//                        InvalidLogins = entity.InvalidLogins,
//                        ForcePwdChange = entity.ForcePwdChange,
//                        LockedOut = entity.LockedOut,
//                        LastLogin = entity.LastLogin,
//                        PwdChangedDate = entity.PwdChangedDate,
//                        LastModified = entity.LastModified,
//                        CreatedBy = entity.CreatedBy,
//                        CreatedDate = entity.CreatedDate,
//                        LockoutDate = entity.LockoutDate,
//                        ModifiedBy = entity.ModifiedBy,
//                        SuccessfulLogins = entity.SuccessfulLogins
//                    };
//                    return obj;
//                }
//                else
//                {
//                    throw new InvalidOperationException();
//                }
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }

//        public TCoreUsersPasswordObject GetUserDefaultPasswordByUsername(string id)
//        {
//            try
//            {
//                if (!string.IsNullOrEmpty(id))
//                {
//                    int getuser = _context.TCoreUsers.Where(x => x.UserName == id).Select(p => p.UserId).Single();
//                    var entity = _context.TCoreUsersPassword.Find(getuser);

//                    TCoreUsersPasswordObject obj = new TCoreUsersPasswordObject
//                    {
//                        UserId = entity.UserId,
//                        PwdEncrypt = entity.PwdEncrypt,
//                        PwdExpiryDate = entity.PwdExpiryDate,
//                        CumulativeLogins = entity.CumulativeLogins,
//                        InvalidLogins = entity.InvalidLogins,
//                        ForcePwdChange = entity.ForcePwdChange,
//                        LockedOut = entity.LockedOut,
//                        LastLogin = entity.LastLogin,
//                        PwdChangedDate = entity.PwdChangedDate,
//                        LastModified = entity.LastModified,
//                        CreatedBy = entity.CreatedBy,
//                        CreatedDate = entity.CreatedDate,
//                        LockoutDate = entity.LockoutDate,
//                        ModifiedBy = entity.ModifiedBy,
//                        SuccessfulLogins = entity.SuccessfulLogins
//                    };
//                    return obj;
//                }
//                else
//                {
//                    throw new InvalidOperationException();
//                }
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }
//        public TCoreUsersPasswordObject GetUserDefaultPasswordByProfile(int? userId)
//        {

//            try
//            {
//                if (userId.HasValue)
//                {
//                    var entity = _userdefaultpasswordRepository.GetBy(x => x.UserId == userId);
//                    TCoreUsersPasswordObject obj = new TCoreUsersPasswordObject
//                    {
//                        UserId = entity.UserId,
//                        PwdEncrypt = entity.PwdEncrypt,
//                        PwdExpiryDate = entity.PwdExpiryDate,
//                        CumulativeLogins = entity.CumulativeLogins,
//                        InvalidLogins = entity.InvalidLogins,
//                        ForcePwdChange = entity.ForcePwdChange,
//                        LockedOut = entity.LockedOut,
//                        LastLogin = entity.LastLogin,
//                        PwdChangedDate = entity.PwdChangedDate,
//                        LastModified = entity.LastModified,
//                        CreatedBy = entity.CreatedBy,
//                        CreatedDate = entity.CreatedDate,
//                        LockoutDate = entity.LockoutDate,
//                        ModifiedBy = entity.ModifiedBy,
//                        SuccessfulLogins = entity.SuccessfulLogins
//                    };
//                    //var entity = Mapper.Map<TUserDefaultPassword, TUserDefaultPasswordObject>(entity);
//                    return obj;
//                }
//                else
//                {
//                    throw new InvalidOperationException();
//                }
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }

//        public IEnumerable<TCoreUsersPasswordObject> GetUserDefaultPasswords(int id)
//        {
//            try
//            {
//                //var entities = _userdefaultpasswordRepository.Get(x => x.LockedOut == true, x => x.OrderBy(y => y.LastModified),null);
//                //var entities = _userdefaultpasswordRepository.Get(p => p.UserId == id, x => x.OrderBy(y => y.LastModified), null);
//                var entities = _userdefaultpasswordRepository.Get(p => p.UserId == id, x => x.OrderBy(y => y.LastModified), null);
//                IList<TCoreUsersPasswordObject> List = new List<TCoreUsersPasswordObject>();
//                foreach (var entity in entities)
//                {
//                    TCoreUsersPasswordObject obj = new TCoreUsersPasswordObject
//                    {
//                        UserId = entity.UserId,
//                        PwdEncrypt = entity.PwdEncrypt,
//                        PwdExpiryDate = entity.PwdExpiryDate,
//                        CumulativeLogins = entity.CumulativeLogins,
//                        InvalidLogins = entity.InvalidLogins,
//                        ForcePwdChange = entity.ForcePwdChange,
//                        LockedOut = entity.LockedOut,
//                        LastLogin = entity.LastLogin,
//                        PwdChangedDate = entity.PwdChangedDate,
//                        LastModified = entity.LastModified,
//                        CreatedBy = entity.CreatedBy,
//                        CreatedDate = entity.CreatedDate,
//                        LockoutDate = entity.LockoutDate,
//                        ModifiedBy = entity.ModifiedBy,
//                        SuccessfulLogins = entity.SuccessfulLogins
//                    };
//                    //var obj = Mapper.Map<TUserDefaultPassword, TUserDefaultPasswordObject>(entity);
//                    //obj.User = entity.Staff.FirstName + " " + entity.Staff.MiddleName + " " + entity.Staff.SurName;
//                    List.Add(obj);
//                }
//                return List.ToList();
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }
//        public bool MailingPassword(TCoreUsersPasswordObject obj)
//        {
//            try
//            {
//                var entity = _context.TCoreUsersPassword.Find(obj.UserId);
//                if (entity != null)
//                {
//                    entity.LastModified = DateTime.Now;
//                    _userdefaultpasswordRepository.Update(entity);
//                    _userdefaultpasswordRepository.Save();
//                    return true;
//                }
//                else
//                {
//                    throw new InvalidOperationException();
//                }
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }
//        public IEnumerable<TCoreUsersPasswordObject> GetUserDefaultPasswordsById(int? id)
//        {
//            try
//            {
//                var entities = _userdefaultpasswordRepository.Get(x => x.UserId == id, null);
//                IList<TCoreUsersPasswordObject> List = new List<TCoreUsersPasswordObject>();
//                foreach (var entity in entities)
//                {
//                    TCoreUsersPasswordObject obj = new TCoreUsersPasswordObject
//                    {
//                        UserId = entity.UserId,
//                        PwdEncrypt = entity.PwdEncrypt,
//                        PwdExpiryDate = entity.PwdExpiryDate,
//                        CumulativeLogins = entity.CumulativeLogins,
//                        InvalidLogins = entity.InvalidLogins,
//                        ForcePwdChange = entity.ForcePwdChange,
//                        LockedOut = entity.LockedOut,
//                        LastLogin = entity.LastLogin,
//                        PwdChangedDate = entity.PwdChangedDate,
//                        LastModified = entity.LastModified,
//                        CreatedBy = entity.CreatedBy,
//                        CreatedDate = entity.CreatedDate,
//                        LockoutDate = entity.LockoutDate,
//                        ModifiedBy = entity.ModifiedBy,
//                        SuccessfulLogins = entity.SuccessfulLogins
//                    };
//                    //var obj = Mapper.Map<TUserDefaultPassword, TUserDefaultPasswordObject>(entity);
//                    List.Add(obj);
//                }
//                return List.ToList();
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }
//        public bool UpdateUserDefaultPassword(TCoreUsersPasswordObject obj)
//        {
//            var entity1 = _context.TCoreUsersPassword.Find(obj.UserId);
//            try
//            {
//                if (entity1 != null)
//                {
//                    TCoreUsersPassword entity = new TCoreUsersPassword
//                    {
//                        UserId = obj.UserId,
//                        PwdEncrypt = obj.PwdEncrypt,
//                        PwdExpiryDate = obj.PwdExpiryDate,
//                        PwdChangedDate = obj.PwdChangedDate,
//                        LastModified = obj.LastModified,
//                        ModifiedBy = obj.ModifiedBy
//                    };
//                    _userdefaultpasswordRepository.Update(entity);
//                    _userdefaultpasswordRepository.Save();
//                    return true;
//                }
//                else
//                {
//                    throw new InvalidOperationException();
//                }
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }
//        #endregion

//        #region User Old Passwords


//        public TCoreUsersPasswordHistObject GetUserOldPassword(int? id)
//        {

//            try
//            {
//                if (id.HasValue)
//                {
//                    var entity = _useroldpasswordRepository.GetById(id.Value);
//                    var obj = Mapper.Map<TCoreUsersPasswordHist, TCoreUsersPasswordHistObject>(entity);

//                    return obj;
//                }
//                else
//                {
//                    throw new InvalidOperationException();
//                }
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }
//        public TCoreUsersPasswordHistObject GetUserOldPasswordByProfile(int? userId)
//        {

//            try
//            {
//                if (userId.HasValue)
//                {
//                    var entity = _useroldpasswordRepository.GetBy(x => x.UserId == userId, p => p.User);
//                    var obj = Mapper.Map<TCoreUsersPasswordHist, TCoreUsersPasswordHistObject>(entity);

//                    return obj;
//                }
//                else
//                {
//                    throw new InvalidOperationException();
//                }
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }
//        public IEnumerable<TCoreUsersPasswordHistObject> GetUserOldPasswords(int? id)
//        {
//            try
//            {

//                var entities = _useroldpasswordRepository.Get(x => x.UserId == id, null, p => p.User);
//                IList<TCoreUsersPasswordHistObject> List = new List<TCoreUsersPasswordHistObject>();
//                foreach (var entity in entities)
//                {
//                    var obj = Mapper.Map<TCoreUsersPasswordHist, TCoreUsersPasswordHistObject>(entity);
//                    List.Add(obj);
//                }
//                return List.ToList();
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }
//        public bool UpdateUserOldPassword(TCoreUsersPasswordHistObject obj)
//        {
//            var entity = _useroldpasswordRepository.GetById(obj.UserId);
//            try
//            {
//                if (entity != null)
//                {
//                    entity.LastModified = DateTime.Now;
//                    //entity.HasExpired = true;

//                    _useroldpasswordRepository.Update(entity);
//                    _useroldpasswordRepository.Save();
//                    return true;
//                }
//                else
//                {
//                    throw new InvalidOperationException();
//                }
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }

//        public IEnumerable<PasswordOptionsObject> GetPasswordOptions()
//        {
//            try
//            {
//                var entities = _passwordOptionRepository.Get(null, null);
//                IList<PasswordOptionsObject> List = new List<PasswordOptionsObject>();
//                foreach (var entity in entities)
//                {
//                    var obj = Mapper.Map<PasswordOptions, PasswordOptionsObject>(entity);
//                    //obj.User = entity.Staff.FirstName + " " + entity.Staff.MiddleName + " " + entity.Staff.SurName;
//                    List.Add(obj);
//                }
//                return List.ToList();
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }

//        public PasswordOptionsObject GetPasswordOption(int? id)
//        {
//            try
//            {
//                if (id.HasValue)
//                {
//                    var entity = _passwordOptionRepository.GetById(id.Value);
//                    var obj = Mapper.Map<PasswordOptions, PasswordOptionsObject>(entity);
//                    return obj;
//                }
//                throw new InvalidOperationException($"id - {id} doesn't exist in the system!");
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }

//        public bool UpdatePasswordOption(PasswordOptionsObject obj)
//        {
//            var entity = _passwordOptionRepository.GetById(obj.Id);
//            try
//            {
//                if (entity != null)
//                {
//                    entity.LastModified = DateTime.Now;
//                    _passwordOptionRepository.Update(entity);
//                    _passwordOptionRepository.Save();
//                    return true;
//                }
//                else
//                {
//                    throw new InvalidOperationException();
//                }
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }
//        #endregion

//        #region Dynamic Password Table Updaters
//        public void IncUserDefaultPasswordInvalidLogin(int? userId, int? Invalid, string UserName)
//        {
//            try
//            {
//                //SQL CLient Operation
//                var connection = _configura.GetConnectionString("OSMLiteDbConnection");
//                SqlConnection con = new SqlConnection(connection);
//                con.Open();

//                //Date Converter
//                DateTime mydateconverter = DateTime.Now;
//                string sqlformattedDate = mydateconverter.ToString("yyyy-MM-dd HH:mm:ss.fff");

//                String queryString = "UPDATE t_core_users_password SET invalid_logins=@val,modified_by=@ModifiedBy,last_modified=@Date WHERE user_id=@uinstid;";
//                SqlCommand cmd = new SqlCommand(queryString, con);
//                cmd.Parameters.AddWithValue("@val", Invalid.Value);
//                cmd.Parameters.AddWithValue("@ModifiedBy", UserName);
//                cmd.Parameters.AddWithValue("@Date", sqlformattedDate);
//                cmd.Parameters.AddWithValue("@uinstid", userId.Value);
//                cmd.ExecuteNonQuery();
//                con.Close();
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }

//        public void IncUserDefaultPasswordSuccessfulLogin(TCoreUsersPasswordObject obj)
//        {
//            throw new NotImplementedException();
//        }

//        public void IncUserDefaultPasswordCumulativeLogin(int? userId, int? Cumulative, string UserName)
//        {
//            try
//            {
//                //SQL CLient Operation
//                var connection = _configura.GetConnectionString("OSMLiteDbConnection");
//                SqlConnection con = new SqlConnection(connection);
//                con.Open();

//                //Date Converter
//                DateTime mydateconverter = DateTime.Now;
//                string sqlformattedDate = mydateconverter.ToString("yyyy-MM-dd HH:mm:ss.fff");

//                String queryString = "UPDATE t_core_users_password SET cumulative_logins=@val,modified_by=@ModifiedBy,last_modified=@Date WHERE user_id=@uinstid;";
//                SqlCommand cmd = new SqlCommand(queryString, con);
//                cmd.Parameters.AddWithValue("@val", Cumulative.Value);
//                cmd.Parameters.AddWithValue("@ModifiedBy", UserName);
//                cmd.Parameters.AddWithValue("@Date", sqlformattedDate);
//                cmd.Parameters.AddWithValue("@uinstid", userId);
//                cmd.ExecuteNonQuery();
//                con.Close();
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }
//        #endregion
//    }
//}
