namespace Full_proj.repository;
using Full_proj.Domain_Models;
using Full_proj.DtoModels;
public interface IHistoryMessageRepository
{
    public Task<MessageHistoryDto> LoadMessage(string id);
}