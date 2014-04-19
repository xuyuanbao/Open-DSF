using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OpenDSF.Common.Entities
{
    /*
     * After a lot of thinking, I've decided entities will implement ISerializable rather than using serialization attributes, or some other kind of serialization library.
     * 
     * I did consider using a high-performance serialization library like protobuf.net, as well as just allowing classes implementing the Entity interface
     * to just use serialization attributes.
     * 
     * The reason I decided not to go with protobut.net is while its excellent serialization/deserialization speed make it seem like an ideal candidate for this project
     * one of the major reasons for it's blinding speed is how it goes about encoding type information on the wire.
     * 
     * First up, it does not support serialization/deserialization via a base interface type like Entity - which is not too big a deal, I could choose to make Entity an abstract base
     * class and work around this limitation. While from a pure OO point of view I would prefer Entity to be an interface (Entity doesn't implement any behaviour), at the least it makes 
     * sense to claim that the base class of (say) a Vehicle is Entity so I won't violate the Liskov Substitution Principle.
     * 
     * The more serious issue is that if you want to use inheritance, the "ProtoInclude" attribute used to inform the serialization library of an inheritance relationship between two classes
     * requires you supply a system-wide unique numeric identifier. *** This is a major reason why protobuf.net is so fast ***. For small classes, encoding the type as either a string or some
     * kind of GUID would add bytes to the encoding on the wire. So protobuf.net is indeed heavily optimized for speed, but is sacrificing flexibility in order to achieve it.
     * 
     * Unfortunately, for the kind of design I have in mind I really need that extra flexibility. First up, I have no idea whatsoever what the class hierarchy rooted at the Entity interface will
     * look like. It's a simulation FRAMEWORK, not a specific simulator - so in principle, anybody using this framework could choose to extend the Entity class hierarchy. Thinking even further ahead
     * (I have a dream...) I'm expecting people will implement entity classes as DLLs, which would allow entity types to be shared between users of the framework. That's not going to work well if you 
     * have a constraint which says that types need a human-generated, unique ID for each type - how will they avoid ID collisions if they don't even know each other's classes exist at the time they 
     * are written?
     * 
     * The reason I decided to explicitly include ISerializable as a base interface is probably a bit of OO snobbery on my part :). It just doesn't make sense for a child class of Entity *not* to be
     * serializable, and making Entity inherit ISerializable states that intention clearly.
     */
    public interface Entity : ISerializable
    {
        // This ID is unique for a given simulation
        long EntityID { get; }

        // Being a parent of an entity implies you "contain" it, so don't go creating cycles of parents :)
        Entity Parent { get; set; }
    }
}
