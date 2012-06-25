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
  [Command("AuthorizeFeed", "Adds login/password into NuGet settings")]
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

    private const string METADATA = "/$metadata";

    protected override void ExecuteCommandImpl()
    {
      Func<string, String> prepare = url => url.TrimEnd('/');
      Func<string, string, bool> urlEquals = (x,y) => prepare(x).Equals(prepare(y), StringComparison.InvariantCultureIgnoreCase);

      var allSources = SourceProvider.LoadPackageSources().ToArray();
      
      Func<PackageSource, bool> predicate = x => urlEquals(Source, x.Source);
      Func<PackageSource, bool> filter = x => urlEquals(Source + METADATA, x.Source);
   
      var source = allSources.FirstOrDefault(predicate);
      if (source == null)
      {
        Console.WriteLine("No such source was found. Creating new source...");
        source = new PackageSource(Source, Source, true);
      }

      var metadata = new PackageSource(Source + METADATA, Source + "_metadata", false);
      var newFeeds = new[] {source, metadata};
      foreach (var s in newFeeds)
      {
        s.UserName = UserName;
        s.Password = Password;
      }

      SourceProvider.SavePackageSources(newFeeds.Union(allSources.Where(x=>!predicate(x) && !filter(x))).ToArray());
    }
  }
}
