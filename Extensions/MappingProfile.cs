using AutoMapper;
using TestProgrammasy.DTOs;
using TestProgrammasy.Models;

namespace MyLibrary.Extensions
{
    public class MappingProfile : Profile
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
                .ForMember(p => p.StudentName, p => p.MapFrom(p => p.User.FirstName + " " + p.User.LastName))
                .ForMember(p => p.StudentClass, p => p.MapFrom(p => p.User.Class))
                .ForMember(p => p.Type, p => p.MapFrom(p => p.Test.Type));
        }
    }
}
