import { ThumbsUp, ThumbsDown, MessageSquareText, Star, PenLine, Trash2 } from 'lucide-react';
import { addComment, getCommentsForReview, deleteComment } from "../../../api/comment";
import { getReviewUserLikeStatus, toggleReviewLike, updateReview, deleteReview } from "../../../api/review";
import { updateComment } from "../../../api/comment";
import CommentList from "./CommentList.jsx";
import { AddComment } from "./AddComment.jsx";
import EditReviewModal from "./EditReview.jsx";
import DeleteReviewModal from "./DeleteReview.jsx";
import { useEffect, useState } from "react";
import { useAuth } from "../../../context/AuthContext";
import { useNavigate } from "react-router";
import EditComment from "./EditComment.jsx";
import DeleteComment from "./DeleteComment.jsx";

export default function Review({ review }) {
  const navigate = useNavigate();
  const { userId, user, token, isAdmin } = useAuth();

  // On garde une copie locale de la review pour pouvoir la mettre à jour
  const [localReview, setLocalReview] = useState(review);
  const [comments, setComments] = useState([]);
  const [text, setText] = useState("");
  const [showComments, setShowComments] = useState(false);

  // Like / Dislike
  const [likesCount, setLikesCount] = useState(review.likesCount ?? 0);
  const [dislikesCount, setDislikesCount] = useState(review.dislikesCount ?? 0);
  const [userReaction, setUserReaction] = useState(null); // "like" | "dislike" | null
  const [isLikeProcessing, setIsLikeProcessing] = useState(false);

  // Modals édition / suppression
  const [isEditing, setIsEditing] = useState(false);
  const [isDeleting, setIsDeleting] = useState(false);
  const [editTitle, setEditTitle] = useState(review.title);
  const [editContent, setEditContent] = useState(review.content);
  const [editRating, setEditRating] = useState(review.rating);

  // Masquer la review localement après suppression
  const [isDeleted, setIsDeleted] = useState(false);

  useEffect(() => {
    setLocalReview(review);
    setLikesCount(review.likesCount ?? 0);
    setDislikesCount(review.dislikesCount ?? 0);
    if (review.userReaction === "like" || review.userReaction === "dislike") {
      setUserReaction(review.userReaction);
    } else {
      setUserReaction(null);
    }
  }, [review]);

  useEffect(() => {
    const load = async () => {
      try {
        const data = await getCommentsForReview(review.id);
        setComments(data);
      } catch (e) {
        console.error("Erreur chargement commentaires", e);
      }
    };
    load();
  }, [review.id]);

useEffect(() => {
    if (!token || !userId) return;

    const fetchReaction = async () => {
      try {
        const res = await getReviewUserLikeStatus(localReview.id, userId, token);


        if (!res) {
          setUserReaction(null);
          return;
        }

        if (res.liked === true) {
          setUserReaction("like");
        } else if (res.liked === false) {
          setUserReaction("dislike");
        } else {
          setUserReaction(null);
        }

        if (typeof res.likesCount === "number") setLikesCount(res.likesCount);
        if (typeof res.dislikesCount === "number") setDislikesCount(res.dislikesCount);

      } catch (e) {
        console.error("Erreur chargement réaction utilisateur:", e);
        setUserReaction(null);
      }
    };

    fetchReaction();
}, [localReview.id, userId, token]);

  const handleAddComment = async (e) => {
    if (e) e.preventDefault();
    if (!token || !userId) return; // sécurité : doit être connecté
    if (text.trim().length < 1) return;

    try {
      const created = await addComment(
        review.id,
        userId ?? user?.id,
        { content: text },
        token
      );



      setComments(prev => [
        ...prev,
        {
          id: created.id,
          content: created.content,
          createdAt: created.createdAt,
          username: created.username,
          avatarUrl: created.avatarUrl,
        },
      ]);

      setText("");
    } catch (e) {
      console.error("Erreur ajout commentaire", e);
    }
  };

  const handleLike = async (liked) => {
    if (!token || !userId) return;

    if (isLikeProcessing) return;

    setIsLikeProcessing(true);

    try {
      const res = await toggleReviewLike(localReview.id, userId, liked, token);

      if (!res) {
        console.error("Réponse invalide du backend");
        return;
      }

      // Mise à jour stricte depuis les valeurs renvoyées par l’API
      if (typeof res.likesCount === "number") setLikesCount(res.likesCount);
      if (typeof res.dislikesCount === "number") setDislikesCount(res.dislikesCount);

      // Interprétation de la réaction côté backend
      if (res.liked === true) {
        setUserReaction("like");
      } else if (res.liked === false) {
        setUserReaction("dislike");
      } else {
        setUserReaction(null);
      }
    } catch (e) {
      console.error("Erreur like/dislike", e);
    } finally {
      setIsLikeProcessing(false);
    }
  };

  const isOwner = localReview.userId === userId || isAdmin;

  const handleEditSubmit = async (e) => {
    e.preventDefault();
    if (!token || !userId) return;

    try {
      const updated = await updateReview(
        localReview.id,
        userId,
        {
          title: editTitle,
          content: editContent,
          rating: editRating,
        },
        token
      );

      setLocalReview(prev => ({
        ...prev,
        title: updated.title,
        content: updated.content,
        rating: updated.rating,
      }));

      setIsEditing(false);
    } catch (e) {
      console.error("Erreur modification critique", e);
    }
  };

  const handleDeleteConfirm = async () => {
    if (!token || !userId) return;

    try {
      await deleteReview(localReview.id, userId, token);
      setIsDeleting(false);
      setIsDeleted(true);
    } catch (e) {
      console.error("Erreur suppression critique", e);
    }
  };

  const [isEditCommentOpen, setIsEditCommentOpen] = useState(false);
  const [isDeleteCommentOpen, setIsDeleteCommentOpen] = useState(false);
  const [commentToEdit, setCommentToEdit] = useState(null);
  const [commentToDelete, setCommentToDelete] = useState(null);
  const [editCommentContent, setEditCommentContent] = useState("");

  const handleEditComment = async (comment) => {
    setCommentToEdit(comment);
    setEditCommentContent(comment.content);
    setIsEditCommentOpen(true);
  };

  const handleDeleteComment = async (commentId) => {
    setCommentToDelete(commentId);
    setIsDeleteCommentOpen(true);
  };

  const submitEditComment = async (e) => {
    e.preventDefault();
    try {
      const updated = await updateComment(
        commentToEdit.id,
        userId ?? user?.id,
        { content: editCommentContent },
        token
      );
      setComments(prev =>
        prev.map(c => c.id === commentToEdit.id ? { ...c, content: updated.content } : c)
      );
      setIsEditCommentOpen(false);
    } catch (e) {
      console.error("Erreur modification commentaire", e);
    }
  };
  const confirmDeleteComment = async () => {
    try {
      await deleteComment(commentToDelete, userId ?? user?.id, isAdmin, token);
      setComments(prev => prev.filter(c => c.id !== commentToDelete));
      setIsDeleteCommentOpen(false);
    } catch (e) {
      console.error("Erreur suppression commentaire", e);
    }
  };

  if (isDeleted) {
    return null;
  }

  return (
    <div className="flex gap-3 sm:gap-4 text-white py-6 border-b border-gray-700 w-[90%] sm:w-[80%] max-w-6xl mx-auto">
      {/* Avatar */}
      <div className="flex flex-col items-center mt-12 sm:mt-8">
        <img
          src={localReview.avatarUrl}
          alt={localReview.username}
          className="w-10 h-10 sm:w-20 sm:h-20 rounded-full object-cover cursor-pointer"
          onClick={() => navigate(`/profil/${localReview.userId}`)}
        />
      </div>

      <div className="flex-1 min-w-0 mt-1">
        <div className="flex items-center gap-3 mb-1">
          <p
            className="font-semibold cursor-pointer"
            onClick={() => navigate(`/profil/${localReview.userId}`)}
          >
            {localReview.username}
          </p>
        </div>

        {/* Bulle de texte */}
        <div className="bg-white text-black p-3 sm:p-5 rounded-xl mt-3 w-full shadow-md relative bubble text-sm sm:text-base">
          <div className="absolute top-3 right-3 flex gap-2">
            {isOwner && (
              <>
                <button onClick={() => setIsEditing(true)} className="text-gray-600 hover:text-black cursor-pointer">
                  <PenLine size={16} />
                </button>
                <button onClick={() => setIsDeleting(true)} className="text-red-500 hover:text-red-700 cursor-pointer">
                  <Trash2 size={16} />
                </button>
              </>
            )}
          </div>
          <h3 className="font-bold mb-1">{localReview.title}</h3>

          <p className="flex items-center gap-1 text-accentuation font-bold mb-2">
            <Star size={16} /> {localReview.rating}/10
          </p>

          <p>{localReview.content}</p>
        </div>

        {/* Actions like/dislike/commentaires */}
        <div className="flex items-center gap-6 sm:gap-12 mt-3 text-accentuation font-semibold justify-center text-xs sm:text-sm">
          <button
            className={`flex items-center gap-1 hover:text-accentuation cursor-pointer ${
              userReaction === "like" ? "text-accentuation" : ""
            }`}
            onClick={() => handleLike(true)}
            disabled={isLikeProcessing}
          >
            <ThumbsUp size={13} fill={userReaction === "like" && likesCount > 0 ? "currentColor" : "none"} />
            <span>{likesCount}</span>
          </button>

          <button
            className={`flex items-center gap-1 hover:text-accentuation cursor-pointer ${
              userReaction === "dislike" ? "text-accentuation" : ""
            }`}
            onClick={() => handleLike(false)}
            disabled={isLikeProcessing}
          >
            <ThumbsDown size={13} fill={userReaction === "dislike" && dislikesCount > 0 ? "currentColor" : "none"} />
            <span>{dislikesCount}</span>
          </button>

          <button
            className="flex items-center gap-1 hover:text-accentuation cursor-pointer"
            onClick={() => setShowComments(prev => !prev)}
          >
            <MessageSquareText size={13} />
            <span>{comments.length}</span>
          </button>
        </div>

        {/* Zone commentaires */}
        {showComments && (
          <div className="ml-10 mt-4">
            <div className="flex mt-6">
              {/* Trait vertical */}
              <div className="flex flex-col items-center mr-6">
                <div className="w-px bg-accentuation h-full"></div>
              </div>

              {/* Commentaires */}
              <div className="flex-1 space-y-4">
                <CommentList
                  comments={comments}
                  userId={userId}
                  isAdmin={isAdmin}
                  onEdit={handleEditComment}
                  onDelete={handleDeleteComment}
                />

                {token && (
                  <div className="flex gap-3 mt-6 max-w-lg">
                    <AddComment
                      text={text}
                      setText={setText}
                      handleSubmit={handleAddComment}
                    />
                  </div>
                )}
              </div>
            </div>
          </div>
        )}

        <EditComment
          isOpen={isEditCommentOpen}
          onClose={() => setIsEditCommentOpen(false)}
          onSubmit={submitEditComment}
          content={editCommentContent}
          setContent={setEditCommentContent}
        />
        <DeleteComment
          isOpen={isDeleteCommentOpen}
          onClose={() => setIsDeleteCommentOpen(false)}
          onConfirm={confirmDeleteComment}
        />

        {/* Modal édition */}
        {isEditing && (
          <EditReviewModal
            isOpen={isEditing}
            onClose={() => setIsEditing(false)}
            localReview={localReview}
            editTitle={editTitle}
            setEditTitle={setEditTitle}
            editContent={editContent}
            setEditContent={setEditContent}
            editRating={editRating}
            setEditRating={setEditRating}
            onSubmit={handleEditSubmit}
          />
        )}

        {/* Modal suppression */}
        {isDeleting && (
          <DeleteReviewModal
            isOpen={isDeleting}
            onClose={() => setIsDeleting(false)}
            onConfirm={handleDeleteConfirm}
          />
        )}
      </div>
    </div>
  );
}