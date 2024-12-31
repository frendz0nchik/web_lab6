using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

// ������� ��������� Startup � �������� ConfigureServices
var startup = new Startup();
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// �������� Configure �� Startup
startup.Configure(app);

app.Run();
