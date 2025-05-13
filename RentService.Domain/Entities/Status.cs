using System.ComponentModel.DataAnnotations;

namespace RentService.Domain.Entities
{
    /// <summary>
    /// Типы статусов для аренды/заказов
    /// </summary>
    /// <remarks>
    /// Используется для строгой типизации статусов в системе.
    /// Соответствует записям в таблице Statuses в базе данных.
    /// </remarks>
    public enum StatusType
    {
        /// <summary>
        /// Активная аренда/заказ
        /// </summary>
        /// <remarks>
        /// Используется когда аренда активна и оплачена
        /// </remarks>
        [Display(Name = "Активный", Description = "Аренда активна и оплачена")]
        Active = 1,

        /// <summary>
        /// Успешно завершенная аренда
        /// </summary>
        /// <remarks>
        /// Устанавливается когда товар возвращен и аренда завершена
        /// </remarks>
        [Display(Name = "Возврат", Description = "Товар возвращен, аренда завершена")]
        Completed = 2,

        /// <summary>
        /// Просроченная аренда
        /// </summary>
        /// <remarks>
        /// Устанавливается когда срок аренды истек, но товар не возвращен
        /// </remarks>
        [Display(Name = "Просрочена", Description = "Срок аренды истек, товар не возвращен")]
        Cancelled = 3
    }
    public class Status
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }
    }
}
