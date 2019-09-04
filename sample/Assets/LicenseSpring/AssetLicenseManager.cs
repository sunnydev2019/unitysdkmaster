﻿using System.IO;

using UnityEngine;
using UnityEngine.Windows;

using LicenseSpring;

namespace LicenseSpring.Unity
{

    //tentative names, this will be move out to seperate dll and repacked and probably generated 
    //and installed to client machines
    [DefaultExecutionOrder(-100), ExecuteAlways]
    public class AssetLicenseManager : MonoBehaviour
    {
        private static AssetLicenseManager _INSTANCE;
        private GameObject _licenseHolder;
        private string _api,
                                _skey,
                                _prodCode,
                                _appName,
                                _appVersion;

        private LicenseSpringConfiguration _licenseConfig;

        public static AssetLicenseManager Instance
        {
            get
            {
                return _INSTANCE;
            }
        }

        public bool IsInitialized { get; private set; }

        public License CurrentLicense
        {
            get
            {
                return (License)LicenseManager.CurrentLicense();
            }
        }

        public LicenseManager LicenseManager { get; private set; }

        public AssetLicenseManager()
        {
            //all client specific api will be 'burn-in' into this api behaviour and made into a dll
            _api = "afce72fb-9fba-406e-8d19-ffde5b0a7cad";
            _skey = "Qc8EdU7DY-gMI87-JMueZWXdtJ0Ek_hS6dGC_SwusO8";
            _prodCode = "udu";
            _appName = "Unity Asset Store Item Licensor";
            _appVersion = "v.1.0";

            _licenseConfig = new LicenseSpringConfiguration(_api, _skey,
                _prodCode,
                _appName,
                _appVersion);

            this.LicenseManager = (LicenseManager)LicenseManager.GetInstance();
        }

        private void Awake()
        {
            //TODO :license path, this still producing errors, had to run as administrator which is very unlikely to happen
            LicenseSpringExtendedOptions licenseSpringExtendedOptions = new LicenseSpringExtendedOptions
            {
                HardwareID = System.Guid.NewGuid().ToString(),
                EnableLogging = true,
                CollectHostNameAndLocalIP = true,
                LicenseFilePath = Application.persistentDataPath
            };

            //configuring
            this.LicenseManager.Initialize(_licenseConfig);
            //initializing manually
            IsInitialized = LicenseManager.IsInitialized();

            //this where we hook to unity engine internal script initialization
            if (_INSTANCE == null || _INSTANCE != this)
                _INSTANCE = this;

            _licenseHolder = new GameObject("SpringLicense");
            _licenseHolder.AddComponent<LicenseInfo>();

            DontDestroyOnLoad(this);
        }
    } 
}
