using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using advancedWpfProject.Services;
using advancedWpfProject.MVVM.Model;
using advancedWpfProject.MVVM.ViewModel;

namespace advancedWpfProject
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context,services) => {
                    ConfigureServices(services);
                }).Build();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();

            services.AddTransient<IDaySchedulerViewModel,DaySchedulerViewModel>();
            services.AddTransient<IWeekSchedulerViewModel, WeekSchedulerViewModel>();
            services.AddTransient<IMonthSchedulerViewModel, MonthSchedulerViewModel>();
            services.AddTransient<IInputFormViewModel, InputFormViewModel>();
            services.AddTransient<ILoginFormViewModel, LoginFormViewModel>();
            services.AddTransient<IRegisterFormViewModel, RegisterFormViewModel>();
            services.AddTransient<IModifyEventFormViewModel, ModifyEventFormViewModel>();


            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<ILoginRepository, LoginRepository>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
            MainWindow mainWindow =_host.Services.GetRequiredService<MainWindow>();
            mainWindow.DataContext = _host.Services.GetRequiredService<MainViewModel>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync();
            }

            base.OnExit(e);
        }

    }
}
