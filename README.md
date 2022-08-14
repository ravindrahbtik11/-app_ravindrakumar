create a new repository on the command line
echo "# Test12" >> README.md
git init
git add README.md
git commit -m "first commit"
git branch -M master
git remote add origin https://github.com/ravindrahbtik11/app_ravindrakumar.git
git push -u origin master



push an existing repository from the command line

git remote add origin https://github.com/ravindrahbtik11/app_ravindrakumar.git
git branch -M master
git push -u origin master



The default branch has been renamed!
main is now named master

If you have a local clone, you can update it by running the following commands.

git branch -m main master
git fetch origin
git branch -u origin/master master
git remote set-head origin -a