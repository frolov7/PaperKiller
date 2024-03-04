using AutoMapper;
using PaperKiller.DTO;
using PaperKiller.Models;
using PaperKiller.Models.items;
using PaperKiller.Models.linen;
using System.ComponentModel;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegistrationDTO, Student>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore()) // Пропустить UserId
            .ForMember(dest => dest.UserType, opt => opt.Ignore()) // Пропустить UserType
            .ForMember(dest => dest.CheckInDate, opt => opt.Ignore()) // Пропустить CheckInDate
            .ForMember(dest => dest.LinenId, opt => opt.Ignore()) // Пропустить LinenId
            .ForMember(dest => dest.ItemsId, opt => opt.Ignore()) // Пропустить ItemsId
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone)) // Сопоставить Phone с PhoneNumber
            .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentID)) // Сопоставить StudentID
            .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.RoomNumber)) // Сопоставить RoomNumber
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password)) // Сопоставить Password
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)) // Сопоставить Name
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname)) // Сопоставить Surname
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender)); // Сопоставить Gender

        CreateMap<AuthorizationDTO, Student>()
            .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

        CreateMap<StudentChangeDataDTO, Student>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NewName))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.NewSurname))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.NewPhone));
    }
}
