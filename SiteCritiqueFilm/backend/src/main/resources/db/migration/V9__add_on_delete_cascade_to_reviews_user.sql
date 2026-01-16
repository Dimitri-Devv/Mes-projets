ALTER TABLE reviews
DROP CONSTRAINT IF EXISTS reviews_user_id_fkey;
ALTER TABLE reviews
ADD CONSTRAINT reviews_user_id_fkey
FOREIGN KEY (user_id)
REFERENCES users(id)
ON DELETE CASCADE;

ALTER TABLE comments
DROP CONSTRAINT IF EXISTS comments_user_id_fkey;
ALTER TABLE comments
ADD CONSTRAINT comments_user_id_fkey
FOREIGN KEY (user_id)
REFERENCES users(id)
ON DELETE CASCADE;

ALTER TABLE review_likes
DROP CONSTRAINT IF EXISTS review_likes_user_id_fkey;
ALTER TABLE review_likes
ADD CONSTRAINT review_likes_user_id_fkey
FOREIGN KEY (user_id)
REFERENCES users(id)
ON DELETE CASCADE;
