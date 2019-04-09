/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using AppStatistics.Models;
using AppStatistics.Tizen.Wearable.Services.Privilege;

[assembly: Xamarin.Forms.Dependency(typeof(PrivilegeService))]

namespace AppStatistics.Tizen.Wearable.Services.Privilege
{
    /// <summary>
    /// Allows to check whether all permissions have been granted.
    /// Implements IPrivilegeService interface.
    /// </summary>
    public class PrivilegeService : IPrivilegeService
    {
        #region methods

        /// <summary>
        /// Returns true if all permissions have been granted, false otherwise.
        /// </summary>
        /// <returns>True if all permissions have been granted, false otherwise.</returns>
        public bool AllPermissionsGranted()
        {
            return PrivilegeManager.Instance.AllPermissionsGranted();
        }

        #endregion
    }
}
