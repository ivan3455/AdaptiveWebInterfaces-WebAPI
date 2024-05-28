using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AdaptiveWebInterfaces_WebAPI.Services.Health_Check
{
    public class ResourceUsageHealthCheck : IHealthCheck
    {

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            // Ось ваша логіка перевірки ресурсів.
            // Тут ви можете виконати перевірку пам'яті, процесора, мережевого трафіку і т. д.

            // Приклад: перевірка пам'яті (для ілюстрації)
            double memoryUsage = GC.GetTotalMemory(false) / (1024.0 * 1024.0); // Конвертуємо байти у мегабайти
            bool memoryUsageExceeded = memoryUsage > 500; // Припустимо, що ліміт - 500 мегабайт

            // Повертаємо результат перевірки
            if (memoryUsageExceeded)
            {
                // Якщо ресурси вичерпані, повертаємо статус Failed і додаткову інформацію, що спричинило проблему
                return Task.FromResult(HealthCheckResult.Unhealthy("Memory usage exceeds the limit."));
            }
            else
            {
                // Якщо ресурси не вичерпані, повертаємо статус Healthy
                return Task.FromResult(HealthCheckResult.Healthy());
            }
        }
    }
}
