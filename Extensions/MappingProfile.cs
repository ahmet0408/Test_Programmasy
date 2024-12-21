using AutoMapper;
using System.Linq;
using TestProgrammasy.DTOs;
using TestProgrammasy.Models;

namespace MyLibrary.Extensions
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateTestDTO, Test>();
            CreateMap<EditTestDTO, Test>().ReverseMap();
            CreateMap<TestDTO, Test>().ReverseMap();

            CreateMap<CreateQuestionDTO, Question>();
            CreateMap<Question, QuestionDTO>().ReverseMap();
            CreateMap<Question, EditQuestionDTO>().ReverseMap();

            CreateMap<CreateAnswerDTO, Answer>();
            CreateMap<Answer, AnswerDTO>().ReverseMap();
            CreateMap<Answer, EditAnswerDTO>().ReverseMap();

            CreateMap<TestResultDTO, TestResult>();
            CreateMap<TestResult, TestResultDTO>()
                .ForMember(p => p.StudentName, p => p.MapFrom(p => p.User.FirstName + " " + p.User.LastName));
                

            //CreateMap<CreateAboutDTO, About>();
            //CreateMap<AboutTranslateDTO, AboutTranslate>().ReverseMap();
            //CreateMap<EditAboutDTO, About>().ReverseMap();
            //CreateMap<About, AboutDTO>()
            //    .ForMember(p => p.Title, p => p.MapFrom(p => p.AboutTranslates.Select(p => p.Title).FirstOrDefault()))
            //    .ForMember(p => p.Description, p => p.MapFrom(p => p.AboutTranslates.Select(p => p.Description).FirstOrDefault()));

        }
    }
}
