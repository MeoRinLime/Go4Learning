namespace back.Modules
{
    public class Result<T>
    {
        private int code; //编码：1成功，0和其它数字为失败

        private string? msg; //错误信息

        private T? data; //数据

        public int Code { get => code; set => code = value; }
        public string Msg { get => msg; set => msg = value; }
        public T Data { get => data; set => data = value; }

        public static   Result<T> success(T obj)
        {
            Result<T> r = new Result<T>();
            r.data = obj;
            r.code = 1;
            return r;
        }

        public static  Result<T> error(String msg)
        {
            Result<T> r = new Result<T>();
            r.msg = msg;
            r.code = 0;
            return r;
        }

      
    }
}
