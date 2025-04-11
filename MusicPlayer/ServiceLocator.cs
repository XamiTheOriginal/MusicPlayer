using System;
using Microsoft.Extensions.DependencyInjection;

namespace MusicPlayer
{
    /// <summary>
    /// Fournit un accès statique au conteneur de services pour l'injection de dépendances.
    /// </summary>
    public static class ServiceLocator
    {
        /// <summary>
        /// Instance du fournisseur de services (DI container).
        /// </summary>
        public static IServiceProvider? Instance { get; private set; }

        /// <summary>
        /// Initialise le ServiceLocator avec le fournisseur de services.
        /// </summary>
        /// <param name="provider">Le fournisseur de services à utiliser.</param>
        public static void Init(IServiceProvider? provider)
        {
            if (Instance != null)
            {
                Console.WriteLine("⚠️ ServiceLocator est déjà initialisé. Réinitialisation interdite.");
                return;
            }

            Instance = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        /// <summary>
        /// Récupère un service du type demandé à partir du conteneur.
        /// </summary>
        /// <typeparam name="T">Le type du service à récupérer.</typeparam>
        /// <returns>L'instance du service.</returns>
        public static T Get<T>() where T : notnull
        {
            if (Instance == null)
                throw new InvalidOperationException("ServiceLocator n’a pas été initialisé.");

            return Instance.GetRequiredService<T>();
        }
    }
}