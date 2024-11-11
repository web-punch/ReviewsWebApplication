using Reviews.Domain.Models;

namespace Reviews.Domain.Interfaces
{
    public interface IReviewService
    {
        /// <summary>
        /// Получение всех отзывов по продукту
        /// </summary>
        /// <param name="id">Id продукта</param>
        /// <returns>Коллекция отзывов</returns>
        Task<List<Review>> GetReviewsByProductIdAsync(Guid id);

        /// <summary>
        /// Получение отзыва
        /// </summary>
        /// <param name="id">Id отзыва</param>
        /// <returns>Отзыв</returns>
        Task<Review?> GetReviewAsync(Guid id);

        /// <summary>
        /// Удаление отзыва
        /// </summary>
        /// <param name="id">Id отзыва</param>
        /// <returns>Логическое (true или false) значение</returns>
        Task<bool> TryDeleteReviewAsync(Guid id);

        /// <summary>
        /// Добавление отзыва
        /// </summary>
        /// <param name="review">Модель отзыва</param>
        /// <returns>Логическое (true или false) значение</returns>
        Task<bool> TryAddReviewAsync(Review review);
    }
}
