﻿// See https://aka.ms/new-console-template for more information
using Ninject;
using System.Reflection;
using YandexCloud.BD;
using YandexCloud.CORE;

StandardKernel standartKernel = new StandardKernel();

//standartKernel.Load(Assembly.GetExecutingAssembly());
//IDB idb = standartKernel.Get<DB>();
//var base_cl = new BL(idb);
//base_cl.BasisLogik();

standartKernel.Bind<IBL>().To<BL>().InTransientScope();
standartKernel.Bind<IDB>().To<DB>().InTransientScope();

var baseCl = standartKernel.Get<IBL>();
baseCl.BasisLogik();
