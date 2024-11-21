using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using milestone3library.Data;
using milestone3library.Interface;
using milestone3library.Repository;
using milestone3library.Service;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<libraryDbcontext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("SQL")));

builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IMemberService, MemberService>();

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();


builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IGenreService, GenreService>();


builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<IBookTransactionRepository, BookTransactionRepository>();
builder.Services.AddScoped<IBookTransactionService, BookTransactionService>();


//builder.Services.AddScoped<IEmailLogRepository, EmailLogRepository>();
//builder.Services.AddScoped<IEmailLogService, EmailLogService>();
// Registering EmailService


builder.Services.AddControllers();

//var key = new SymmetricSecurityKey(ASCIIEncoding.ASCII.GetBytes(builder.Configuration["Jwt:key"]));

//builder.Services.AddAuthentication()
//    .AddBearerToken(opt =>
//    opt.BearerTokenProtector = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//    {
//        ValidateIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidateAudience = builder.Configuration["Jwt:Audience"],
//        ValidateIssuerSigningKey = key
//    });

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("Allow",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        });
});

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

app.UseCors("Allow");

app.Run();
