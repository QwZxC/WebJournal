﻿using FirstWebAPI.Models.DTO;
using FirstWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemeController : ControllerBase
    {

        #region HTTPGets

        [HttpGet("All", Name = "GetAllThemes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ThemeDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ThemeDTO>> GetAllThemes()
        {
            IEnumerable<ThemeDTO> themes = Plan.Themes.Select(theme => new ThemeDTO()
            {
                Id = theme.Id,
                Name = theme.Name,
                LessonId= theme.LessonId,
            });
            return Ok(themes);
        }

        [HttpGet("name:string", Name = "GetThemeByName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ThemeDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<LessonDTO>> GetThemeByName(string name)
        {
            IEnumerable<ThemeDTO> themes = Plan.Themes.FindAll(theme => theme.Name == name).Select(lesson => new ThemeDTO()
            {
                Id = lesson.Id,
                Name = lesson.Name,
                LessonId = lesson.LessonId,
            });
            if (!themes.Any())
            {
                return NotFound($"Темы с именем '{name}' нет");
            }
            return Ok(themes);
        }

        [HttpGet("{id:int}", Name = "GetThemeById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThemeDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ThemeDTO> GetThemeById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            Theme Theme = Plan.Themes.Find(thm => thm.Id == id);
            if (Theme == null)
            {
                return NotFound($"Темы с id = {id} не существует");
            }
            return Ok(new ThemeDTO()
            {
                Id = Theme.Id,
                Name = Theme.Name,
                LessonId = Theme.LessonId,
            });
        }

        #endregion

        #region HTTPDeletes

        [HttpDelete("name:string", Name = "DeleteThemeByName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ThemeDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ThemeDTO>> DeleteThemeByName(string name)
        {
            List<Theme> themes = Plan.Themes.FindAll(theme => theme.Name == name);

            themes.ToList().ForEach(theme =>
            {
                Plan.Lessons.Find(lesson => lesson.Id == theme.LessonId).Themes.Remove(theme);
                Plan.Themes.Remove(theme);
            });

            if (!themes.Any()) {
                return NotFound($"Занятий с именем '{name}' не существует ");
            }

            IEnumerable<ThemeDTO> newThemes = Plan.Themes.FindAll(theme => theme.Name == name).Select(theme => new ThemeDTO()
            {
                Id = theme.Id,
                Name = theme.Name,
                LessonId = theme.LessonId
            });

            return Ok(newThemes);
        }

        [HttpDelete("{id:int}", Name = "DeleteThemeById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ThemeDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ThemeDTO>> DeleteThemeById(int id)
        {
            if (id <= 0) {
                return BadRequest("Неверный Id");
            }

            Theme theme = Plan.Themes.Find(les => les.Id == id);
            if (theme == null) {
                return NotFound($"Темы с id = {id} не существует");
            }

            Plan.Lessons.Find(lesson => lesson.Themes.Remove(theme)).Themes.Remove(theme);

            Plan.Themes.Remove(theme);
            
            IEnumerable<ThemeDTO> themes = Plan.Themes.Select(theme => new ThemeDTO()
            {
                Id = theme.Id,
                Name = theme.Name,
                LessonId = theme.LessonId
            });
            return Ok(themes);
        }

        #endregion

        #region HTTPPost

        [HttpPost]
        [Route("Create", Name = "CreateTheme")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LessonDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(LessonDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(LessonDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(LessonDTO))]
        public ActionResult<LessonDTO> CreateTheme([FromBody] ThemeDTO model)
        {
            if (model == null)
                return BadRequest();

            Lesson lesson = Plan.Lessons.Find(lesson => lesson.Id == model.LessonId);

            if (lesson == null)
                return NotFound($"Занятия с Id = {model.LessonId} не найдено");

            Theme theme = new()
            {
                Id = model.Id,
                Name = model.Name,
                LessonId = model.LessonId,
            };

            lesson.Themes.Add(theme);

            return Ok(lesson);
        }

        #endregion
    }
}