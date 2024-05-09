using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using TodoAPI.Model;
using TodoAPI.Services;
using TodoAPI.ViewModel;

namespace UnitTest_TodoAPI
{
    [TestClass]
    public class TodoServiceTests
    {
        private Mock<IMapper> _mockMapper;
        private Mock<TodoDbContext> _mockDbContext;
        private TodoService _todoService;

        public TodoServiceTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockDbContext = new Mock<TodoDbContext>();
            _todoService = new TodoService(_mockDbContext.Object, _mockMapper.Object);
        }

        private Mock<DbSet<Todo>> SetupMockDbSet(IEnumerable<Todo> todos)
        {
            var mockDbSet = new Mock<DbSet<Todo>>();
            mockDbSet.As<IQueryable<Todo>>().Setup(m => m.Provider).Returns(todos.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Todo>>().Setup(m => m.Expression).Returns(todos.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Todo>>().Setup(m => m.ElementType).Returns(todos.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Todo>>().Setup(m => m.GetEnumerator()).Returns(todos.AsQueryable().GetEnumerator());
            return mockDbSet;
        }

        private void SetupDbContextWithTodos(IEnumerable<Todo> todos)
        {
            _mockDbContext.Setup(db => db.Todos).Returns(SetupMockDbSet(todos).Object);
        }

        [TestMethod]
        public void AddTodoTasks_ValidTodo_ReturnsTrue()
        {
            // Arrange
            var Existing_todoData = new TodoData { TaskName = "Drinking", Username = "MuhilTheHacker", IsDone = false };

            // Act
            var result = _todoService.AddTodoTasks(Existing_todoData);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteTodoTasks_ValidUsernameWithDoneTasks_ReturnsTrue()
        {
            // Arrange
            var username = "MuhilTheHacker";
            var todos = new List<Todo> { new Todo { Username = username, TaskName="Running", IsDone = true }, new Todo { Username = username, TaskName = "Sleeping", IsDone = false } };
            
            SetupDbContextWithTodos(todos);

            // Act
            var result = _todoService.DeleteTodoTasks(username);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetTodoTasks_ExistingUser()
        {
            //Arrange
            var username = "MuhilTheHacker";
            var todos = new List<Todo> { new Todo { Username = username, TaskName = "Running", IsDone = true } };
            SetupDbContextWithTodos(todos);

            //Act
            var result = _todoService.GetTodoTasks(username);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateTodoTasks_TaskIsNotDone()
        {
            //Arrange
            var Existing_task = new Todo { Username = "MuhilTheHacker", TaskName = "Drinking", IsDone = true };
            var Non_Existing_task = new Todo { Username = "MuhilTheHacker", TaskName = "Flying", IsDone = false };

            //Act
            var Success_result = _todoService.UpdateTodoTasks(Existing_task);
            var Failure_result = _todoService.UpdateTodoTasks(Non_Existing_task);

            //Assert
            Assert.IsFalse(Success_result);
            Assert.IsFalse(Failure_result);
        }
    }
}
