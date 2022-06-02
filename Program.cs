namespace DITest6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseDefaultServiceProvider((context, options) => options.ValidateOnBuild = true);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient(typeof(IBase<bool>), typeof(AnotherHandler<bool>));
            builder.Services.AddTransient(typeof(IBase<>), typeof(Handler<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            var services = app.Services;

            var child1Handlers = services.GetServices<IBase<bool>>().ToList();

            app.Run();
        }
    }

    public interface IBase<T> { }

    public class Handler<T> : IBase<T> { }

    public class AnotherHandler<T> : IBase<T> { }
}