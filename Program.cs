using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using IS_SISTEMA.controllers;
using IS_SISTEMA.models;
using System.Text.Json;

var mysqlconnection = Database.Connect();


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();

app.MapGet("/", async (HttpContext context) => {
    var controller = new PageController();
    var result = controller.Index();

    await result.ExecuteResultAsync(new ActionContext {
        HttpContext = context,
        RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
        ActionDescriptor = new ActionDescriptor()
    });


});
app.MapGet("/lecturerdashboard", async (HttpContext context) => {
    var controller = new PageController();
    var result = controller.LecturerIndex();

    await result.ExecuteResultAsync(new ActionContext {
        HttpContext = context,
        RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
        ActionDescriptor = new ActionDescriptor()
    });


});

app.MapGet("/groups", async (HttpContext context) => {
    var controller = new PageController();
    var result = controller.Groups();

    await result.ExecuteResultAsync(new ActionContext {
        HttpContext = context,
        RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
        ActionDescriptor = new ActionDescriptor()
    });


});
app.MapPost("/api/newgroups", async (HttpContext context) => {
    var form = await context.Request.ReadFormAsync(); 

    var groups = new Groups(
        form["Shortcut"].FirstOrDefault() ?? string.Empty,
        form["Name"].FirstOrDefault() ?? string.Empty
    );
    Groups.ITable(mysqlconnection, groups);

    context.Response.Redirect("/groups");
});
app.MapPost("/api/delgroups", async(HttpContext context) => {
    var form = await context.Request.ReadFormAsync();
    if(int.TryParse(form["id"].FirstOrDefault(), out int groupsId)) {
        Groups.Delete(mysqlconnection, groupsId);
        context.Response.Redirect("/groups");
    }
    else {
        context.Response.StatusCode = 400;
    }
});
app.MapPost("/api/group-lecturers/{group_id}/{lecturer_id}", async (int group_id, int lecturer_id, HttpContext context) => {
    Groups.InsertLecturer(mysqlconnection, group_id, lecturer_id);
   
    await context.Response.WriteAsync($"{group_id} {lecturer_id}");
});
app.MapGet("/lecturers", async (HttpContext context) => {
    var controller = new PageController();
    var result = controller.Lecturers();

    await result.ExecuteResultAsync(new ActionContext {
        HttpContext = context,
        RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
        ActionDescriptor = new ActionDescriptor()
    });


});
app.MapPost("/api/newlecturers", async (HttpContext context) => {
    var form = await context.Request.ReadFormAsync(); 

    var lecturers = new Lecturers(
        form["Name"].FirstOrDefault() ?? string.Empty,
        form["LastName"].FirstOrDefault() ?? string.Empty
);
    Lecturers.ITable(mysqlconnection, lecturers);

context.Response.Redirect("/lecturers");
});
app.MapPost("/api/dellecturers", async(HttpContext context) => {
    var form = await context.Request.ReadFormAsync();
    if(int.TryParse(form["id"].FirstOrDefault(), out int lecturersId)) {
        Lecturers.Delete(mysqlconnection, lecturersId);
        context.Response.Redirect("/lecturers");
    }
    else {
        context.Response.StatusCode = 400;
    }
});
app.MapGet("/students", async (HttpContext context) => {
    var controller = new PageController();
    var result = controller.Students();

    await result.ExecuteResultAsync(new ActionContext {
        HttpContext = context,
        RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
        ActionDescriptor = new ActionDescriptor()
    });     
});
app.MapPost("/api/newstudents", async (HttpContext context) => {
    var form = await context.Request.ReadFormAsync(); 

    var students = new Students(
        form["Name"].FirstOrDefault() ?? string.Empty,
        form["LastName"].FirstOrDefault() ?? string.Empty,
        form["Phone"].FirstOrDefault() ?? string.Empty
);
    Students.ITable(mysqlconnection, students);

context.Response.Redirect("/students");
});
app.MapPost("/api/delstudents", async(HttpContext context) => {
    var form = await context.Request.ReadFormAsync();
    if(int.TryParse(form["id"].FirstOrDefault(), out int studentsId)) {
        Students.Delete(mysqlconnection, studentsId);
        context.Response.Redirect("/students");
    }
    else {
        context.Response.StatusCode = 400;
    }
});
app.MapPost("/api/group-students/{group_id}/{student_id}", async (int group_id, int student_id, HttpContext context) => {
    Groups.InsertStudent(mysqlconnection, group_id, student_id);
   
    await context.Response.WriteAsync($"{group_id} {student_id}");
});
app.MapGet("/subjects", async (HttpContext context) => {
    var controller = new PageController();
    var result = controller.Subjects();

    await result.ExecuteResultAsync(new ActionContext {
        HttpContext = context,
        RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
        ActionDescriptor = new ActionDescriptor()
    });


});
app.MapPost("/api/newsubjects", async (HttpContext context) => {
    var form = await context.Request.ReadFormAsync(); 

    var subjects = new Subjects(
        form["Name"].FirstOrDefault() ?? string.Empty
    );
    Subjects.ITable(mysqlconnection, subjects);

    context.Response.Redirect("/subjects");
});

app.MapPost("/api/delsubjects", async(HttpContext context) => {
    var form = await context.Request.ReadFormAsync();
    if(int.TryParse(form["id"].FirstOrDefault(), out int subjectsId)) {
        Subjects.Delete(mysqlconnection, subjectsId);
        context.Response.Redirect("/subjects");
    }
    else {
        context.Response.StatusCode = 400;
    }
});
app.MapPost("/api/group-subjects/{group_id}/{subject_id}", async (int group_id, int subject_id, HttpContext context) => {
    Groups.InsertSubject(mysqlconnection, group_id, subject_id);

    await context.Response.WriteAsync($"{group_id} {subject_id}");
});
app.MapGet("/api/groups/{groupId}/students", async (int groupId, HttpContext context) =>
{
    var students = Groups.GetStudentsByGroupId(mysqlconnection, groupId);

    var jsonResponse = JsonSerializer.Serialize(students);
    context.Response.ContentType = "application/json";
    await context.Response.WriteAsync(jsonResponse);
});
app.Run();
