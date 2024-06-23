using Microsoft.EntityFrameworkCore;

using Contman.Api;
using Contman.Api.Models;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Contman.Api.Models.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteDbConnection"))
);

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddScoped<IValidator<Contact>, ContactValidator>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    );
}

//app.UseHttpsRedirection();

// endpoint: Get all contacts
app.MapGet("/api/contacts", async ([FromQuery] string? filter, ApplicationDbContext dbContext) =>
{
    List<Contact> contacts;

    if (string.IsNullOrWhiteSpace(filter))
        contacts = await dbContext.Contacts.ToListAsync();
    else
        contacts = await dbContext.Contacts.Where(x =>
               !string.IsNullOrWhiteSpace(x.Name) && x.Name.ToLower().IndexOf(filter.ToLower()) >= 0
            || !string.IsNullOrWhiteSpace(x.Email) && x.Email.ToLower().IndexOf(filter.ToLower()) >= 0
            || !string.IsNullOrWhiteSpace(x.Phone) && x.Phone.ToLower().IndexOf(filter.ToLower()) >= 0
        ).ToListAsync();

    return Results.Ok(contacts);
})
.WithName("GetAllContacts")
.WithOpenApi();


// endpoint: Get single contact by id
app.MapGet("/api/contacts/{id}", async (int id, ApplicationDbContext dbContext) =>
{
    var contact = await dbContext.Contacts.FindAsync(id);
    return Results.Ok(contact);
})
.WithName("GetContactById")
.WithOpenApi();

// endpoint: Create a new contact
app.MapPost("/api/contacts", async (IValidator<Contact> validator, Contact contact, HttpContext context, ApplicationDbContext dbContext) =>
{
    // Validations
    var validationResult = await validator.ValidateAsync(contact);

    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    // Save changes
    dbContext.Contacts.Add(contact);
    await dbContext.SaveChangesAsync();

    var baseUrl = context.Request.Host.ToString() + "/api";
    return Results.Created($"{baseUrl}/contacts/{contact.Id}", contact);
})
.WithName("CreateNewContact")
.WithOpenApi();

// endpoint: Update contact by id
app.MapPut("/api/contacts/{id}", async ([FromQuery] int id, [FromBody] Contact contact, HttpContext context, IValidator <Contact> validator, ApplicationDbContext dbContext) =>
{
    var contactToUpdate = await dbContext.Contacts.FindAsync(id);

    if (contactToUpdate is null) return Results.NotFound();

    contactToUpdate.Name = contact.Name ?? contactToUpdate.Name;
    contactToUpdate.Email = contact.Email ?? contactToUpdate.Email;
    contactToUpdate.Phone = contact.Phone ?? contactToUpdate.Phone;
    contactToUpdate.Address = contact.Address ?? contactToUpdate.Address;

    // Validations
    var validationResult = await validator.ValidateAsync(contactToUpdate);

    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    await dbContext.SaveChangesAsync();
    
    var baseUrl = context.Request.Host.ToString() + "/api";
    return Results.Ok($"{baseUrl}/contacts/{contactToUpdate.Id}");
})
.WithName("UpdateContact")
.WithOpenApi();

// endpoint: Delete contact by id
app.MapDelete("/api/contacts/{id}", async (int id, ApplicationDbContext dbContext) =>
{
    var contactToUpdate = await dbContext.Contacts.FindAsync(id);

    if (contactToUpdate is null) return Results.NotFound();

    dbContext.Contacts.Remove(contactToUpdate);
    
    var status = (await dbContext.SaveChangesAsync()) > 0;
    
    if (status)
        return Results.NoContent();

    return Results.StatusCode(StatusCodes.Status500InternalServerError);
})
.WithName("RemoveContact")
.WithOpenApi();

app.Run();

