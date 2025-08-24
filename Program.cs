using BlogApi.Data;
using BlogApi.Middleware;
using BlogApi.Models;
using BlogApi.Profiles;
using BlogApi.Repositories.Interfaces;
using BlogApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Slugify;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ���������� EF
builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ���������� UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// ���������� SlugHelper
builder.Services.AddSingleton<SlugHelper>();

// ���������� ������
builder.Services.AddAutoMapper(
        cfg => cfg.AddMaps(
            typeof(CategoryMappingProfile).Assembly,
            typeof(CommentMappingProfile).Assembly,
            typeof(CommentReportMappingProfile).Assembly,
            typeof(LikeMappingProfile).Assembly,
            typeof(PostMappingProfile).Assembly,
            typeof(PostReportMappingProfile).Assembly,
            typeof(ReasonMappingProfile).Assembly,
            typeof(TagMappingProfile).Assembly,
            typeof(UserMappingProfile).Assembly
        )
    );

// ����������� + Swagger
builder.Services.AddControllers();

// ���������� Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
    .AddEntityFrameworkStores<BlogDbContext>()
    .AddDefaultTokenProviders();

// ���������� ����������� � �������
builder.Services.Scan(scan =>
    scan.FromAssembliesOf(
        typeof(ICategoryRepository)
        //typeof(ICommentReportRepository),
        //typeof(ICommentRepository),
        //typeof(ILikeRepository),
        //typeof(IPostReportRepository),
        //typeof(IPostRepository),
        //typeof(IReasonRepository),
        //typeof(ITagRepository)
    )
    .AddClasses(classes => classes.InNamespaces("BlogApi.Repositories"))
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);

builder.Services.Scan(scan =>
    scan.FromAssembliesOf(
        typeof(IApplicationUserService)
        //typeof(ICategoryService),
        //typeof(ICommentReportService),
        //typeof(ICommentService),
        //typeof(ILikeService),
        //typeof(IPostReportService),
        //typeof(IPostService),
        //typeof(IReasonService),
        //typeof(ITagService)
    )
    .AddClasses(classes => classes.InNamespaces("BlogApi.Services"))
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);


var app = builder.Build();


// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ���������� ��������� ������
app.UseErrorHandling();

app.UseHttpsRedirection();

// ��������� ����������� ������(��� ��������)
app.UseStaticFiles();

// �����������
app.UseAuthentication();
app.UseAuthorization();

// ����������� ������������
app.MapControllers();

app.Run();

