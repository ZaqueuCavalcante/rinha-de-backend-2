tag=$(date +"%Y%m%d%H%M")

docker build -t rinha-de-backend-2:$tag -t zaqueucavalcante/rinha-de-backend-2:$tag .
docker push zaqueucavalcante/rinha-de-backend-2:$tag

echo $tag