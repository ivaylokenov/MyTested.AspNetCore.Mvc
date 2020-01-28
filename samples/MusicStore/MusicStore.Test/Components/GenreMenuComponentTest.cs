namespace MusicStore.Test.Components
{
    using Models;
    using MusicStore.Components;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class GenreMenuComponentTest
    {
        [Fact]
        public void ComponentShouldReturnNineGenres()
        {
            MyMvc
                .ViewComponent<GenreMenuComponent>()
                .WithData(data => data
                    .WithEntities(entities => entities.AddRange(GetGenres)))
                .InvokedWith(vc => vc.InvokeAsync())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IEnumerable<string>>()
                    .Passing(g => g.Count() == 9));
        }

        private static IEnumerable<Genre> GetGenres => Enumerable.Range(1, 10).Select(n => new Genre { GenreId = n });
    }
}
