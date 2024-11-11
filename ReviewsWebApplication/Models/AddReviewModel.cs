namespace ReviewsWebApplication.Models
{
    public class AddReviewModel
    {
        /// <summary>
        /// Id продукта
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Id пользователя, оставившего отзыв
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Текст отзыва
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Оценка (количество звезд)
        /// </summary>
        public int Grade { get; set; }
    }
}
