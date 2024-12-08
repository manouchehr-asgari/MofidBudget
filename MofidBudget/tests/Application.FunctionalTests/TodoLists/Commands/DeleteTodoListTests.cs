﻿using MofidBudget.Application.TodoLists.Commands.CreateTodoList;
using MofidBudget.Application.TodoLists.Commands.DeleteTodoList;
using MofidBudget.Domain.Entities;

using static MofidBudget.Application.FunctionalTests.Testing;

namespace MofidBudget.Application.FunctionalTests.TodoLists.Commands;
public class DeleteTodoListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoListId()
    {
        var command = new DeleteTodoListCommand(99);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoList()
    {
        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        await SendAsync(new DeleteTodoListCommand(listId));

        var list = await FindAsync<TodoList>(listId);

        list.Should().BeNull();
    }
}
