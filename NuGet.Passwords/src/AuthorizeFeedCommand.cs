/*
 * Copyright 2012 Eugene Petrenko
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

using System;
using System.ComponentModel.Composition;
using System.Linq;
using NuGet;

namespace EugenePetrenko.NuGet.Passwords
{
  [Command("TeamCity.AuthorizeFeed", "Adds login/password into NuGet settings")]
  public class AuthorizeFeedCommand : CommandBase
  {
    [Option("Feed source")]
    public string Source { get; set; }

    [Option("User Name")]
    public string UserName { get; set; }

    [Option("Password")]
    public string Password { get; set; }

    [Import]
    public IPackageSourceProvider SourceProvider { get; set; }

    protected override void ExecuteCommandImpl()
    {
      var allSources = SourceProvider.LoadPackageSources().ToArray();
      Func<PackageSource, bool> predicate = x => Source.Equals(x.Source, StringComparison.InvariantCultureIgnoreCase);
   
      var source = allSources.FirstOrDefault(predicate);

      if (source == null)
      {
        Console.WriteLine("No such source was found. Creating new source...");
        source = new PackageSource(Source, Source, true);
      }

      source.UserName = UserName;
      source.Password = Password;

      SourceProvider.SavePackageSources(
        new[]{source}.Union(allSources.Where(x=>!predicate(x))).ToArray()
        );
    }
  }
}
