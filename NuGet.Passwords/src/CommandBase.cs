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
using NuGet;
using NuGet.Commands;

namespace EugenePetrenko.NuGet.Passwords
{
  public abstract class CommandBase : Command
  {
    public sealed override void ExecuteCommand()
    {
      try
      {
        ExecuteCommandImpl();
      } catch(Exception e)
      {
        System.Console.Error.WriteLine("Failed to execute commnad: " + e.Message);
        System.Console.Error.WriteLine(e);
        throw new CommandLineException("TeamCity command failed");
      }
    }

    protected abstract void ExecuteCommandImpl();
  }
}