using AutoMapper;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.DataAccess.Entities;
using System.Linq;

namespace Store.BusinessLogic.Mappers
{
    public class PrintingEditionProfile : Profile
    {
        public PrintingEditionProfile()
        {
            CreateMap<PrintingEditionModel, PrintingEdition>()
                .ForMember(printingEdition => printingEdition.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(printingEdition => printingEdition.Currency, opt => opt.MapFrom(c => c.Currency))
                .ForMember(printingEdition => printingEdition.Description, opt => opt.MapFrom(c => c.Description))
                .ForMember(printingEdition => printingEdition.IsRemoved, opt => opt.MapFrom(c => c.IsRemoved))
                .ForMember(printingEdition => printingEdition.Price, opt => opt.MapFrom(c => c.Price))
                .ForMember(printingEdition => printingEdition.Title, opt => opt.MapFrom(c => c.Title))
                .ForMember(printingEdition => printingEdition.Type, opt => opt.MapFrom(c => c.Type))
                .ForMember(printingEdition => printingEdition.SubtitleReturned, opt => opt.MapFrom(c => string.Empty));
            CreateMap<PrintingEdition, PrintingEditionModel>()
                .ForMember(printingEditionModel => printingEditionModel.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(printingEditionModel => printingEditionModel.Currency, opt => opt.MapFrom(c => c.Currency))
                .ForMember(printingEditionModel => printingEditionModel.Description, opt => opt.MapFrom(c => c.Description))
                .ForMember(printingEditionModel => printingEditionModel.IsRemoved, opt => opt.MapFrom(c => c.IsRemoved))
                .ForMember(printingEditionModel => printingEditionModel.Price, opt => opt.MapFrom(c => c.Price))
                .ForMember(printingEditionModel => printingEditionModel.Title, opt => opt.MapFrom(c => c.Title))
                .ForMember(printingEditionModel => printingEditionModel.Type, opt => opt.MapFrom(c => c.Type))
                .ForMember(printingEditionModel => printingEditionModel.CreationDate, opt => opt.MapFrom(c => c.CreationData))
                .ForMember(printingEditionModel => printingEditionModel.Authors, opt => opt.MapFrom(c => c.AuthorsInPrintingEdition.Select(x => x.Author.Name).ToList()))
                .ForMember(printingEditionModel => printingEditionModel.AuthorsIdList, opt => opt.MapFrom(c => c.AuthorsInPrintingEdition.Select(x => x.Author.Id).ToList()));
        }
    }
}
