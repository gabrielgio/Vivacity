using Octokit;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vivacity.Library.Parser.GitHub
{
    public class GitHubParser : IParser
    {
        private string _owner;

        private string _project;

        private string _user;

        private string _password;

        public GitHubParser(string owner, string project, string user, string password)
        {
            _owner = owner;
            _project = project;
            _password = password;
            _user = user;
        }

        public IEnumerable<IFile> Read()
        {
            var task = ReadAsync();
            task.Wait();
            return task.Result;
        }

        private async Task<IEnumerable<IFile>> ReadAsync()
        {
            List<GitHubFile> files = new List<GitHubFile>();

            var client = new GitHubClient(new ProductHeaderValue(_project));
            var basicAuth = new Credentials(_user, _password);
            client.Credentials = basicAuth;

            var commits = (await client.Repository.Commits.GetAll(_owner, _project)).ToList();

            foreach (var item in commits)
            {
                var commit = await client.Repository.Commits.Get(_owner, _project, item.Sha);

                foreach (var fileItem in commit.Files) 
                {
                    GitHubFile file = files.FirstOrDefault(x => x.Path == fileItem.Filename);
                    if (file == null)
                    {
                        file = new GitHubFile()
                            {
                                Sha = fileItem.Sha,
                                Path = fileItem.Filename,
                                RawDataUrl = fileItem.RawUrl,
                            };

                        files.Add(file);
                    }

                    file.Changes.Add( new Change
                        {
                            AddedLines = fileItem.Additions,
                            DeletedLines = fileItem.Deletions,
                            TotalChanges = fileItem.Changes,
                            ChangeType = fileItem.Status,
                            Date = commit.Commit.Committer.Date
                        });

                }
            }

            files.ForEach(x => x.Date = x.Changes.Min(y => y.Date).Date);

            return files.Cast<IFile>();
        }
    }
}

