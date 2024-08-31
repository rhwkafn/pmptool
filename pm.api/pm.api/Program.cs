using Microsoft.EntityFrameworkCore;
using pm.api.DataModel;
using pm.api.AutoMappers;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(m =>
{
    string path = AppContext.BaseDirectory + "pm.api.xml";
    m.IncludeXmlComments(path, true);
});

//���ݿ�������
builder.Services.AddDbContext<DailyDbContext>(m => m.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));

//autoapperע��
builder.Services.AddAutoMapper(typeof(AutoMapperSettings));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}
 app.UseSwagger();
    app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
