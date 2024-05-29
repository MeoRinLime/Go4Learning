using back.Context;
using back.Modules;

namespace back.Service
{
    public class UserService
    {
        private UserContext userContext;
        public UserService(UserContext userContext) { 
            this.userContext = userContext;
        }

        public Result<string> register(User user)
        {
            if (userContext.user.FirstOrDefault(u => user.UserId == u.UserId) != null)
                return Result<string>.error("该账号已存在");
            try
            {
                userContext.Add(user);
                userContext.SaveChanges();
                return Result<string>.success("注册成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Result<string>.error("注册失败");
            }
        }

        public Result<User> login(User user)
        {
            User? u=userContext.user.FirstOrDefault(u=> user.UserId == u.UserId);
            if (u == null) return Result<User>.error("该账号不存在");
            if(user.UserPassword!=u.UserPassword)
                return   Result<User>.error("密码错误");
            return Result<User>.success(u);
        }
    }
}
